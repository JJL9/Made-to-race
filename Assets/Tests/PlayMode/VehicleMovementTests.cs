using System.Collections;
using MadeToRace.Vehicle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MadeToRace.Tests.PlayMode
{
    /// <summary>
    /// Verifies VehicleController physics behavior: throttle moves the vehicle
    /// forward, braking slows it, steering turns it. Input is fed directly to
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
            _body.mass = 1.2f;
            _body.linearDamping = 0.05f;
            _controller = _vehicle.AddComponent<VehicleController>();

            // A body collider (child) so the vehicle rests on the ground.
            var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            body.transform.SetParent(_vehicle.transform);
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
            yield return Step(40, throttle: 1f, steer: 0f);

            Assert.That(_body.linearVelocity.z, Is.GreaterThan(5f),
                "Full throttle should accelerate the vehicle along +Z within ~0.7s.");
        }

        [UnityTest]
        public IEnumerator Brake_ReducesForwardSpeed()
        {
            yield return Step(40, throttle: 1f, steer: 0f);
            float speedBefore = _body.linearVelocity.z;

            yield return Step(40, throttle: -1f, steer: 0f);
            float speedAfter = _body.linearVelocity.z;

            Assert.That(speedAfter, Is.LessThan(speedBefore),
                "Braking should reduce forward speed.");
        }

        [UnityTest]
        public IEnumerator Steering_TurnsTheVehicle()
        {
            yield return Step(40, throttle: 1f, steer: 1f);

            Assert.That(Mathf.Abs(_body.angularVelocity.y), Is.GreaterThan(0.1f),
                "Steering input should produce yaw rotation while moving.");
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
