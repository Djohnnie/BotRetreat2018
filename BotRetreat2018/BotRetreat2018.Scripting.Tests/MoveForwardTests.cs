using BotRetreat2018.Model;
using BotRetreat2018.Utilities;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BotRetreat2018.Scripting.Tests
{
    [TestClass]
    public class MoveForwardTests
    {
        [TestMethod]
        public async Task Bot_Cannot_MoveForward_With_North_Orientation_On_1x1_Arena()
        {
            // Arrange
            Arena arena = new Arena { Width = 1, Height = 1 };
            Team team = new Team { Name = "Team!" };
            Bot bot = new Bot
            {
                LocationX = 0,
                LocationY = 0,
                Orientation = Orientation.North,
                MaximumStamina = 1,
                CurrentStamina = 1,
                StaminaDrain = 0,
                Arena = arena,
                Team = team
            };

            Script botScript = BotScript.PrepareScript("MoveForward();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot>(), new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.LocationX);
            Assert.AreEqual(0, globals.LocationY);
            Assert.AreEqual(1, globals.Stamina);
            Assert.AreEqual(0, globals.StaminaDrain);
            Assert.AreEqual(Orientation.North, globals.Orientation);
            Assert.AreEqual(LastAction.Idling, globals.CurrentAction);
        }

        [TestMethod]
        public async Task Bot_Cannot_MoveForward_With_East_Orientation_On_1x1_Arena()
        {
            // Arrange
            Arena arena = new Arena { Width = 1, Height = 1 };
            Team team = new Team { Name = "Team!" };
            Bot bot = new Bot
            {
                LocationX = 0,
                LocationY = 0,
                Orientation = Orientation.East,
                MaximumStamina = 1,
                CurrentStamina = 1,
                StaminaDrain = 0,
                Arena = arena,
                Team = team
            };

            Script botScript = BotScript.PrepareScript("MoveForward();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot>(), new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.LocationX);
            Assert.AreEqual(0, globals.LocationY);
            Assert.AreEqual(1, globals.Stamina);
            Assert.AreEqual(0, globals.StaminaDrain);
            Assert.AreEqual(Orientation.East, globals.Orientation);
            Assert.AreEqual(LastAction.Idling, globals.CurrentAction);
        }

        [TestMethod]
        public async Task Bot_Cannot_MoveForward_With_South_Orientation_On_1x1_Arena()
        {
            // Arrange
            Arena arena = new Arena { Width = 1, Height = 1 };
            Team team = new Team { Name = "Team!" };
            Bot bot = new Bot
            {
                LocationX = 0,
                LocationY = 0,
                Orientation = Orientation.South,
                MaximumStamina = 1,
                CurrentStamina = 1,
                StaminaDrain = 0,
                Arena = arena,
                Team = team
            };

            Script botScript = BotScript.PrepareScript("MoveForward();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot>(), new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.LocationX);
            Assert.AreEqual(0, globals.LocationY);
            Assert.AreEqual(1, globals.Stamina);
            Assert.AreEqual(0, globals.StaminaDrain);
            Assert.AreEqual(Orientation.South, globals.Orientation);
            Assert.AreEqual(LastAction.Idling, globals.CurrentAction);
        }

        [TestMethod]
        public async Task Bot_Cannot_MoveForward_With_West_Orientation_On_1x1_Arena()
        {
            // Arrange
            Arena arena = new Arena { Width = 1, Height = 1 };
            Team team = new Team { Name = "Team!" };
            Bot bot = new Bot
            {
                LocationX = 0,
                LocationY = 0,
                Orientation = Orientation.West,
                MaximumStamina = 1,
                CurrentStamina = 1,
                StaminaDrain = 0,
                Arena = arena,
                Team = team
            };

            Script botScript = BotScript.PrepareScript("MoveForward();".Base64Encode());
            botScript.Compile();
            ScriptGlobals globals = new ScriptGlobals(arena, bot, new List<Bot>(), new List<Message>());

            // Act
            await botScript.RunAsync(globals);

            // Assert
            Assert.AreEqual(0, globals.LocationX);
            Assert.AreEqual(0, globals.LocationY);
            Assert.AreEqual(1, globals.Stamina);
            Assert.AreEqual(0, globals.StaminaDrain);
            Assert.AreEqual(Orientation.West, globals.Orientation);
            Assert.AreEqual(LastAction.Idling, globals.CurrentAction);
        }
    }
}