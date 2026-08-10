using System.Collections;
using MadeToRace.Vehicle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MadeToRace.Tests.PlayMode
{
    /// <summary>
    /// Verifies VehicleController behavior with the raycast wheel model:
    /// throttle moves the vehicle forward (traction-limited launch),
    /// braking slows it, steering turns it. Input is fed directly to
    /// Drive() — the input mapping itself is covered in InputDriverTests.
    /// </summary>
    public sealed class VehicleMovementTests
    {
        private GameObject _ground;
        private GameObject _vehicle;
        private Rigidbody _body;
        private VehicleController _controller;

        [SetUp]
        public void SetUp()
        {
            _ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _ground.transform.localScale = new Vector3(100f, 1f, 100f);

            _vehicle = new GameObject("Vehicle");
            _vehicle.transform.position = new Vector3(0f, 2f, 0f);
            _body = _vehicle.AddComponent<Rigidbody>();
            _body.linearDamping = 0.05f;
            _controller = _vehicle.AddComponent<VehicleController>();

            // A body collider (child) so the vehicle can't tunnel through the ground.
            // worldPositionStays=false: the cube must become a centered child of the
            // root — keeping its world position would wedge it into the ground.
            var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            body.transform.SetParent(_vehicle.transform, false);
            body.transform.localScale = new Vector3(2f, 1f, 4f);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_vehicle);
            Object.Destroy(_ground);
        }

        [UnityTest]
        public IEnumerator Throttle_AcceleratesVehicleForward()
        {
            yield return Settle();
            yield return Step(60, throttle: 1f, steer: 0f);

            Assert.That(_body.linearVelocity.z, Is.GreaterThan(3f),
                "Full throttle should accelerate the vehicle forward (~0.7g kart launch).");
        }

        [UnityTest]
        public IEnumerator Brake_ReducesForwardSpeed()
        {
            yield return Settle();
            yield return Step(60, throttle: 1f, steer: 0f);
            float speedBefore = _body.linearVelocity.z;

            yield return Step(40, throttle: -1f, steer: 0f);
            float speedAfter = _body.linearVelocity.z;

            Assert.That(speedAfter, Is.LessThan(speedBefore),
                "Braking should reduce forward speed (grip-limited decel).");
        }

        [UnityTest]
        public IEnumerator Steering_TurnsTheVehicle()
        {
            yield return Settle();
            yield return Step(60, throttle: 1f, steer: 1f);

            Assert.That(Mathf.Abs(_body.angularVelocity.y), Is.GreaterThan(0.1f),
                "Steering input should produce yaw rotation while moving.");
        }

        /// <summary>Lets the vehicle fall onto its suspension and settle.</summary>
        private IEnumerator Settle()
        {
            for (int i = 0; i < 30; i++)
            {
                yield return null;
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator Step(int fixedFrames, float throttle, float steer)
        {
            for (int i = 0; i < fixedFrames; i++)
            {
                _controller.Drive(throttle, steer);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
