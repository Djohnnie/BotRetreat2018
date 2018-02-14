using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2018.Model;
using BotRetreat2018.Utilities;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotRetreat2018.Scripting.Tests
{
    [TestClass]
    public class SelfDestructTests
    {
        [TestMethod]
        public async Task Bot_Can_Self_Destruct()
        {
            // Arrange
            Arena arena = new Arena { Width = 1, Height = 1 };
            Team team = new Team { Name = "Team!" };
            Bot bot = new Bot
            {
                LocationX = 0,
                LocationY = 0,
                CurrentPhysicalHealth = 50,
                Arena = arena,
                Team = team
            };

            Script botScript = await BotScript.PrepareScript("SelfDestruct();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot>(), new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.Kills);
            Assert.AreEqual(0, globals.PhysicalHealth);
            Assert.AreEqual(LastAction.SelfDestruct, globals.CurrentAction);
        }

        [TestMethod]
        public async Task Bot_Can_Damage_Adjacent_Bots_While_Self_Destruct()
        {
            // Arrange
            Arena arena = new Arena { Width = 3, Height = 3 };
            Team myTeam = new Team { Name = "My team!" };
            Team otherTeam = new Team { Name = "Other team!" };
            Bot bot = new Bot { LocationX = 1, LocationY = 1, CurrentPhysicalHealth = 100, Arena = arena, Team = myTeam };
            Bot bot1 = new Bot { LocationX = 0, LocationY = 0, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot2 = new Bot { LocationX = 1, LocationY = 0, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot3 = new Bot { LocationX = 2, LocationY = 0, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot4 = new Bot { LocationX = 0, LocationY = 1, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot5 = new Bot { LocationX = 2, LocationY = 1, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot6 = new Bot { LocationX = 0, LocationY = 2, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot7 = new Bot { LocationX = 1, LocationY = 2, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };
            Bot bot8 = new Bot { LocationX = 2, LocationY = 2, CurrentPhysicalHealth = 25, Arena = arena, Team = otherTeam };

            Script botScript = await BotScript.PrepareScript("SelfDestruct();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot> { bot1, bot2, bot3, bot4, bot5, bot6, bot7, bot8 }, new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.PhysicalHealth);
            Assert.AreEqual(8, globals.Kills);
            Assert.AreEqual(200, globals.PhysicalDamageDone);
            Assert.AreEqual(LastAction.SelfDestruct, globals.CurrentAction);
            Assert.AreEqual(0, bot1.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot2.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot3.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot4.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot5.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot6.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot7.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot8.CurrentPhysicalHealth);
        }

        [TestMethod]
        public async Task Bot_Can_Damage_Adjacent_Bots_1_Position_Away_While_Self_Destruct()
        {
            // Arrange
            Arena arena = new Arena { Width = 5, Height = 5 };
            Team myTeam = new Team { Name = "My team!" };
            Team otherTeam = new Team { Name = "Other team!" };
            Bot bot = new Bot { LocationX = 2, LocationY = 2, CurrentPhysicalHealth = 100, Arena = arena, Team = myTeam };
            Bot bot01 = new Bot { LocationX = 0, LocationY = 0, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot02 = new Bot { LocationX = 1, LocationY = 0, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot03 = new Bot { LocationX = 2, LocationY = 0, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot04 = new Bot { LocationX = 3, LocationY = 0, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot05 = new Bot { LocationX = 4, LocationY = 0, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot06 = new Bot { LocationX = 0, LocationY = 1, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot07 = new Bot { LocationX = 4, LocationY = 1, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot08 = new Bot { LocationX = 0, LocationY = 2, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot09 = new Bot { LocationX = 4, LocationY = 2, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot10 = new Bot { LocationX = 0, LocationY = 3, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot11 = new Bot { LocationX = 4, LocationY = 3, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot12 = new Bot { LocationX = 0, LocationY = 4, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot13 = new Bot { LocationX = 1, LocationY = 4, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot14 = new Bot { LocationX = 2, LocationY = 4, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot15 = new Bot { LocationX = 3, LocationY = 4, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };
            Bot bot16 = new Bot { LocationX = 4, LocationY = 4, CurrentPhysicalHealth = 10, Arena = arena, Team = otherTeam };

            Script botScript = await BotScript.PrepareScript("SelfDestruct();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot> { bot01, bot02, bot03, bot04, bot05, bot06, bot07, bot08, bot09, bot10, bot11, bot12, bot13, bot14, bot15, bot16 }, new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.PhysicalHealth);
            Assert.AreEqual(16, globals.Kills);
            Assert.AreEqual(160, globals.PhysicalDamageDone);
            Assert.AreEqual(LastAction.SelfDestruct, globals.CurrentAction);
            Assert.AreEqual(0, bot01.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot02.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot03.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot04.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot05.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot06.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot07.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot08.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot09.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot10.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot11.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot12.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot13.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot14.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot15.CurrentPhysicalHealth);
            Assert.AreEqual(0, bot16.CurrentPhysicalHealth);
        }
    }
}