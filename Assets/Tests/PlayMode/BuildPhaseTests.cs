using System.Collections;
using MadeToRace.Building;
using MadeToRace.Vehicle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MadeToRace.Tests.PlayMode
{
    /// <summary>
    /// Verifies build-phase consequences (PRD BLD-1/2) with part-driven
    /// physics: a fresh build has no power AND no grip, the engine alone
    /// cannot move the car (no wheels = no traction), and reset restores
    /// a clean unbuildable-to-drive state.
    /// </summary>
    public sealed class BuildPhaseTests
    {
        private GameObject _ground;
        private GameObject _vehicle;
        private Rigidbody _body;
        private BuildPhaseController _build;

        [SetUp]
        public void SetUp()
        {
            _ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _ground.transform.localScale = new Vector3(100f, 1f, 100f);

            _vehicle = new GameObject("Vehicle");
            _vehicle.transform.position = new Vector3(0f, 2f, 0f);
            _body = _vehicle.AddComponent<Rigidbody>();
            _body.linearDamping = 0.05f;

            // Body cube — same as the real prototype vehicle. Without a collider
            // the Rigidbody gets the colliderless DEFAULT inertia tensor (~1),
            // and the CoM's 45/55 rear weight split becomes a 1000 rad/s² pitch
            // explosion instead of a 0.45° static nose-up stance.
            var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            body.name = "Body";
            body.transform.SetParent(_vehicle.transform, false);
            body.transform.localScale = new Vector3(2f, 1f, 4f);

            _vehicle.AddComponent<VehicleController>();
            _build = _vehicle.AddComponent<BuildPhaseController>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_vehicle);
            Object.Destroy(_ground);
        }

        [UnityTest]
        public IEnumerator FreshBuild_IsUnpoweredAndGripless()
        {
            yield return Settle();
            yield return DriveFixedFrames(10);

            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThan(0.01f),
                "A bare chassis must not move: no engine (power) and no wheels (grip).");
        }

        [UnityTest]
        public IEnumerator EngineWithoutWheels_DoesNotMove()
        {
            Assert.That(_build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine), Is.True);

            yield return Settle();
            yield return DriveFixedFrames(10);

            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThan(0.01f),
                "Power with no wheels = no traction — a mechanic would expect no motion.");
        }

        [UnityTest]
        public IEnumerator EngineAndWheels_DrivesTheVehicle()
        {
            _build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine);
            foreach (string slot in VehicleBuild.WheelSlots)
            {
                _build.TryPlace(slot, PartType.Wheel);
            }

            yield return Settle();
            yield return DriveFixedFrames(15); // full launch ramp: wheelie + grip build

            Assert.That(_body.linearVelocity.z, Is.GreaterThan(1f),
                "Engine + wheels must make the vehicle drivable.");
        }

        [UnityTest]
        public IEnumerator RemovingEngine_StopsAcceleration()
        {
            _build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine);
            foreach (string slot in VehicleBuild.WheelSlots)
            {
                _build.TryPlace(slot, PartType.Wheel);
            }

            yield return Settle();
            yield return DriveFixedFrames(30);

            Assert.That(_build.TryRemove(VehicleBuild.EngineSlot), Is.True);
            float speedBefore = _body.linearVelocity.z;

            yield return DriveFixedFrames(10);

            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThanOrEqualTo(Mathf.Abs(speedBefore) + 0.5f),
                "Removing the engine must stop further acceleration.");
        }

        [UnityTest]
        public IEnumerator ResetBuild_RestoresCleanState()
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

            yield return Settle();
            yield return DriveFixedFrames(10);
            Assert.That(Mathf.Abs(_body.linearVelocity.z), Is.LessThan(0.01f),
                "A reset build must not move: no power, no grip.");
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

        private IEnumerator Settle()
        {
            for (int i = 0; i < 30; i++)
            {
                yield return null;
                yield return new WaitForFixedUpdate();
            }
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
