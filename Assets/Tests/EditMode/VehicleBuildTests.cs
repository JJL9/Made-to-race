using System.Collections.Generic;
using System.Linq;
using MadeToRace.Building;
using NUnit.Framework;

namespace MadeToRace.Tests.EditMode
{
    /// <summary>
    /// Pure-logic tests for the vehicle build state (PRD BLD-1/2):
    /// placement rules, occupancy, race-readiness gating, reset.
    /// </summary>
    public sealed class VehicleBuildTests
    {
        private readonly BuildValidator _validator = new BuildValidator();
        private VehicleBuild _build;

        [SetUp]
        public void SetUp()
        {
            _build = new VehicleBuild();
        }

        [Test]
        public void FreshBuild_HasChassisOnly()
        {
            CollectionAssert.AreEqual(new[] { PartType.Chassis }, _build.Parts);
        }

        [Test]
        public void PlaceEngineAndWheels_IsRaceReady()
        {
            PlaceAllParts();
            Assert.That(_build.IsRaceReady(_validator), Is.True);
        }

        [Test]
        public void EngineWithoutWheels_NotRaceReady()
        {
            Assert.That(_build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine), Is.True);
            Assert.That(_build.IsRaceReady(_validator), Is.False);
        }

        [Test]
        public void OccupiedSlot_RejectsSecondPart()
        {
            PlaceAllParts();
            Assert.That(_build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine), Is.False);
            Assert.That(_build.Parts.Count(part => part == PartType.Engine), Is.EqualTo(1));
        }

        [Test]
        public void WrongPartInSlot_Rejected()
        {
            Assert.That(_build.TryPlace(VehicleBuild.EngineSlot, PartType.Wheel), Is.False);
            Assert.That(_build.TryPlace(VehicleBuild.WheelSlots[0], PartType.Engine), Is.False);
        }

        [Test]
        public void Chassis_IsNotRemovableOrPlaceable()
        {
            Assert.That(_build.TryRemove(VehicleBuild.ChassisSlot), Is.False);
            Assert.That(_build.TryPlace("second-chassis", PartType.Chassis), Is.False);
        }

        [Test]
        public void UnknownSlot_Rejected()
        {
            Assert.That(_build.TryPlace("not-a-slot", PartType.Wheel), Is.False);
            Assert.That(_build.TryRemove("not-a-slot"), Is.False);
        }

        [Test]
        public void RemovePart_LosesRaceReady()
        {
            PlaceAllParts();
            foreach (string slot in VehicleBuild.WheelSlots)
            {
                Assert.That(_build.TryRemove(slot), Is.True);
            }
            Assert.That(_build.IsRaceReady(_validator), Is.False,
                "With no wheels left the build must not be race-ready (min 1 wheel).");
        }

        [Test]
        public void Reset_RestoresCleanState_AndAllowsRebuild()
        {
            PlaceAllParts();
            _build.Reset();

            CollectionAssert.AreEqual(new[] { PartType.Chassis }, _build.Parts);
            Assert.That(_build.IsRaceReady(_validator), Is.False);

            PlaceAllParts();
            Assert.That(_build.IsRaceReady(_validator), Is.True);
        }

        private void PlaceAllParts()
        {
            foreach (string slot in VehicleBuild.WheelSlots)
            {
                Assert.That(_build.TryPlace(slot, PartType.Wheel), Is.True);
            }
            Assert.That(_build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine), Is.True);
        }
    }
}
