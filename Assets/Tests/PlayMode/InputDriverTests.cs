using System.Collections;
using MadeToRace.Vehicle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace MadeToRace.Tests.PlayMode
{
    /// <summary>
    /// Verifies PlayerInputDriver maps physical input (keyboard) into Drive()
    /// calls. Uses InputTestFixture's virtual keyboard.
    /// </summary>
    public sealed class InputDriverTests : InputTestFixture
    {
        private GameObject _vehicle;
        private Rigidbody _body;

        [SetUp]
        public void SetUpVehicle()
        {
            _vehicle = new GameObject("Vehicle");
            _vehicle.transform.position = Vector3.zero;
            _body = _vehicle.AddComponent<Rigidbody>();
            _body.useGravity = false; // these tests verify input mapping, not physics
            _vehicle.AddComponent<VehicleController>();
            _vehicle.AddComponent<PlayerInputDriver>();

            // InputTestFixture does not always create a virtual keyboard in
            // batchmode; ensure one exists so Keyboard.current is usable.
            if (Keyboard.current == null)
            {
                InputSystem.AddDevice<Keyboard>();
            }
        }

        [UnityTest]
        public IEnumerator HoldingW_DrivesTheVehicleForward()
        {
            Press(Keyboard.current.wKey);

            // Let the driver's Update read the key, then let physics apply
            // the resulting force (Update runs on frames, forces apply in
            // fixed steps — a single wait can race the ordering).
            for (int i = 0; i < 5; i++)
            {
                yield return null;
                yield return new WaitForFixedUpdate();
            }

            Assert.That(_body.linearVelocity.z, Is.GreaterThan(0f),
                "Holding W should produce forward velocity through the input driver.");
        }

        [UnityTest]
        public IEnumerator NoInput_DoesNotDrive()
        {
            yield return new WaitForFixedUpdate();
            yield return null;

            Assert.That(_body.linearVelocity.magnitude, Is.LessThan(0.1f),
                "With no input the vehicle should not accelerate.");
        }
    }
}
