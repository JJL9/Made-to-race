using System.Collections.Generic;
using MadeToRace.Building;
using NUnit.Framework;

namespace MadeToRace.Tests.EditMode
{
    public sealed class BuildValidatorTests
    {
        private BuildValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new BuildValidator();
        }

        [Test]
        public void RaceReady_WithChassisWheelsAndEngine_IsValid()
        {
            var parts = new List<PartType> { PartType.Chassis, PartType.Wheel, PartType.Engine };

            Assert.That(_validator.IsRaceReady(parts), Is.True);
        }

        [Test]
        public void RaceReady_WithoutEngine_IsInvalid()
        {
            var parts = new List<PartType> { PartType.Chassis, PartType.Wheel };

            Assert.That(_validator.IsRaceReady(parts), Is.False);
        }

        [Test]
        public void RaceReady_WithoutWheels_IsInvalid()
        {
            var parts = new List<PartType> { PartType.Chassis, PartType.Engine };

            Assert.That(_validator.IsRaceReady(parts), Is.False);
        }

        [Test]
        public void RaceReady_Empty_IsInvalid()
        {
            Assert.That(_validator.IsRaceReady(new List<PartType>()), Is.False);
        }
    }
}
