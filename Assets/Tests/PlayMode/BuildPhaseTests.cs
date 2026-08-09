using System.Collections;
using MadeToRace.Building;
using MadeToRace.Vehicle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MadeToRace.Tests.PlayMode
{
    /// <summary>
    /// Verifies build-phase consequences (PRD BLD-1/2): a fresh build has no
    /// power, placing/removing the engine toggles power, and reset restores a
    /// clean unpowered state.
    /// </summary>
    public sealed class BuildPhaseTests
    {
        private GameObject _vehicle;
        private Rigidbody _body;
        private BuildPhaseController _build;

        [SetUp]
        public void SetUp()
        {
            _vehicle = new GameObject("Vehicle");
            _body = _vehicle.AddComponent<Rigidbody>();
            _body.useGravity = false;
            _body.linearDamping = 0.05f;
            _vehicle.AddComponent<VehicleController>();
            _build = _vehicle.AddComponent<BuildPhaseController>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_vehicle);
        }

        [UnityTest]
        public IEnumerator FreshBuild_IsUnpowered()
        {
            yield return DriveFixedFrames(10);

            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThan(0.01f),
                "A chassis with no engine must not accelerate.");
        }

        [UnityTest]
        public IEnumerator PlacingEngine_PowersTheVehicle()
        {
            Assert.That(_build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine), Is.True);

            yield return DriveFixedFrames(10);

            Assert.That(_body.linearVelocity.z, Is.GreaterThan(1f),
                "Attaching the engine must make the vehicle drivable.");
        }

        [UnityTest]
        public IEnumerator RemovingEngine_RemovesPower()
        {
            _build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine);
            yield return DriveFixedFrames(5);

            Assert.That(_build.TryRemove(VehicleBuild.EngineSlot), Is.True);
            float speedBefore = _body.linearVelocity.z;

            yield return DriveFixedFrames(10);

            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThanOrEqualTo(Mathf.Abs(speedBefore) + 0.5f),
                "Removing the engine must stop further acceleration.");
        }

        [UnityTest]
        public IEnumerator ResetBuild_RemovesPower_AndClearsParts()
        {
            _build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine);
            foreach (string slot in VehicleBuild.WheelSlots)
            {
                _build.TryPlace(slot, PartType.Wheel);
            }
            Assert.That(_build.IsRaceReady(), Is.True);

            _build.ResetBuild();

            Assert.That(_build.IsRaceReady(), Is.False);
            CollectionAssert.AreEqual(new[] { PartType.Chassis }, _build.Build.Parts);

            yield return DriveFixedFrames(10);
            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThan(0.01f),
                "A reset build must be unpowered again.");
        }

        [Test]
        public void RaceReady_RequiresEngineAndWheels()
        {
            Assert.That(_build.IsRaceReady(), Is.False);

            _build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine);
            Assert.That(_build.IsRaceReady(), Is.False);

            foreach (string slot in VehicleBuild.WheelSlots)
            {
                _build.TryPlace(slot, PartType.Wheel);
            }
            Assert.That(_build.IsRaceReady(), Is.True);
        }

        private IEnumerator DriveFixedFrames(int frames)
        {
            var controller = _vehicle.GetComponent<VehicleController>();
            for (int i = 0; i < frames; i++)
            {
                controller.Drive(1f, 0f);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
