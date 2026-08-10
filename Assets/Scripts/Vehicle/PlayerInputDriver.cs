using UnityEngine;
using UnityEngine.InputSystem;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// Maps keyboard/gamepad input to VehicleController.Drive().
    /// Thin input layer — no gameplay rules (see docs/ARCHITECTURE.md).
    /// The mapping itself is pure (ComputeInput) and fully unit-tested in
    /// EditMode; this wrapper only reads devices and forwards.
    /// </summary>
    [RequireComponent(typeof(VehicleController))]
    public sealed class PlayerInputDriver : MonoBehaviour
    {
        private VehicleController _vehicle;

        private void Awake()
        {
            _vehicle = GetComponent<VehicleController>();
        }

        /// <summary>
        /// Pure input mapping: keyboard states + gamepad stick → (throttle, steer),
        /// both clamped to [-1, 1]. No device access — deterministic and testable.
        /// </summary>
        public static (float throttle, float steer) ComputeInput(
            bool w, bool s, bool a, bool d,
            bool up, bool down, bool left, bool right,
            Vector2 stick)
        {
            float throttle = 0f;
            if (w || up) throttle += 1f;
            if (s || down) throttle -= 1f;

            float steer = 0f;
            if (a || left) steer -= 1f;
            if (d || right) steer += 1f;

            throttle = Mathf.Clamp(throttle + stick.y, -1f, 1f);
            steer = Mathf.Clamp(steer + stick.x, -1f, 1f);
            return (throttle, steer);
        }

        private void Update()
        {
            if (Keyboard.current == null)
            {
                return;
            }

            Vector2 stick = Gamepad.current != null ? Gamepad.current.leftStick.ReadValue() : Vector2.zero;
            (float throttle, float steer) = ComputeInput(
                Keyboard.current.wKey.isPressed,
                Keyboard.current.sKey.isPressed,
                Keyboard.current.aKey.isPressed,
                Keyboard.current.dKey.isPressed,
                Keyboard.current.upArrowKey.isPressed,
                Keyboard.current.downArrowKey.isPressed,
                Keyboard.current.leftArrowKey.isPressed,
                Keyboard.current.rightArrowKey.isPressed,
                stick);

            _vehicle.Drive(throttle, steer);
        }
    }
}
