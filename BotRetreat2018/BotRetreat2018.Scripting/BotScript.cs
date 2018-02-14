using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat2018.Contracts;
using BotRetreat2018.Model;
using BotRetreat2018.Scripting.Interfaces;
using BotRetreat2018.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Runtime.CompilerServices;

namespace BotRetreat2018.Scripting
{
    public static class BotScript
    {
        public static Task<Script<Object>> PrepareScript(String script)
        {
            return Task.Run(() =>
            {
                var decodedScript = script.Base64Decode();
                var mscorlib = typeof(Object).Assembly;
                var systemCore = typeof(Enumerable).Assembly;
                var botRetreatModel = typeof(Position).Assembly;
                var botRetreatScripting = typeof(IBot).Assembly;
                var dynamic = typeof(DynamicAttribute).Assembly;
                var scriptOptions = ScriptOptions.Default.AddReferences(mscorlib, systemCore, botRetreatModel, botRetreatScripting, dynamic);
                scriptOptions = scriptOptions.WithImports("System", "System.Linq", "System.Collections.Generic", "BotRetreat2018.Model", "BotRetreat2018.Scripting.Interfaces", "System.Runtime.CompilerServices");
                var botScript = CSharpScript.Create(decodedScript, scriptOptions, typeof(ScriptGlobals));
                botScript.WithOptions(botScript.Options.AddReferences(mscorlib, systemCore));
                return botScript;
            });
        }

        public static async Task<ScriptValidationDto> ValidateScript(String script)
        {
            var scriptValidation = new ScriptValidationDto { Script = script, Messages = new List<ScriptValidationMessageDto>() };

            var decodedScript = script.Base64Decode();
            var blockedTokens = new[] { "CSharpCompilation", "dynamic" };
            if (blockedTokens.Any(x => decodedScript.Contains(x)))
            {
                scriptValidation.Messages.Add(new ScriptValidationMessageDto { Message = "Script blocked!" });
                return scriptValidation;
            }

            var botScript = await PrepareScript(script);

            ImmutableArray<Diagnostic> diagnostics;

            using (var sw = new SimpleStopwatch())
            {
                diagnostics = botScript.Compile();
                scriptValidation.CompilationTimeInMilliseconds = sw.ElapsedMilliseconds;
            }

            if (!diagnostics.Any())
            {
                var task = Task.Run(() =>
                {
                    var arena = new Arena { Width = 10, Height = 10, Name = "Arena" };
                    var team = new Team { Name = "MyTeam", Password = "Password" };
                    var enemyTeam = new Team { Name = "EnemyTeam", Password = "Password" };
                    var bot = new Bot
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bot",
                        MaximumPhysicalHealth = 100,
                        CurrentPhysicalHealth = 100,
                        MaximumStamina = 100,
                        CurrentStamina = 100,
                        LocationX = 1,
                        LocationY = 1,
                        Orientation = Orientation.South,
                        Arena = arena,
                        Team = team
                    };
                    var friendBot = new Bot
                    {
                        Id = Guid.NewGuid(),
                        Name = "Friend",
                        MaximumPhysicalHealth = 100,
                        CurrentPhysicalHealth = 100,
                        MaximumStamina = 100,
                        CurrentStamina = 100,
                        LocationX = 1,
                        LocationY = 3,
                        Orientation = Orientation.North,
                        Arena = arena,
                        Team = team
                    };
                    var enemyBot = new Bot
                    {
                        Id = Guid.NewGuid(),
                        Name = "Enemy",
                        MaximumPhysicalHealth = 100,
                        CurrentPhysicalHealth = 100,
                        MaximumStamina = 100,
                        CurrentStamina = 100,
                        LocationX = 1,
                        LocationY = 5,
                        Orientation = Orientation.North,
                        Arena = arena,
                        Team = team
                    };
                    var coreGlobals = new ScriptGlobals(arena, bot, new[] { bot, friendBot, enemyBot }.ToList());
                    using (var sw = new SimpleStopwatch())
                    {
                        try
                        {
                            botScript.RunAsync(coreGlobals).Wait();
                        }
                        catch (Exception ex)
                        {
                            scriptValidation.Messages.Add(new ScriptValidationMessageDto
                            {
                                Message = "Runtime error: " + ex.Message
                            });
                        }
                        scriptValidation.RunTimeInMilliseconds = sw.ElapsedMilliseconds;
                    }
                });

                if (!task.Wait(TimeSpan.FromSeconds(1)))
                {
                    scriptValidation.Messages.Add(new ScriptValidationMessageDto
                    {
                        Message = "Your script did not finish in a timely fashion!"
                    });
                    scriptValidation.RunTimeInMilliseconds = Int64.MaxValue;
                }
            }

            foreach (var d in diagnostics)
            {
                if (d.Severity == DiagnosticSeverity.Error)
                {
                    scriptValidation.Messages.Add(new ScriptValidationMessageDto
                    {
                        Message = d.GetMessage(),
                        LocationStart = d.Location.SourceSpan.Start,
                        LocationEnd = d.Location.SourceSpan.End
                    });
                }
            }

            return scriptValidation;
        }
    }
}