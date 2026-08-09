using UnityEngine;
using UnityEngine.InputSystem;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// Maps keyboard/gamepad input to VehicleController.Drive().
    /// Thin input layer — contains no gameplay rules (see docs/ARCHITECTURE.md).
    /// Keyboard: WASD + arrows. Gamepad: left stick.
    /// </summary>
    [RequireComponent(typeof(VehicleController))]
    public sealed class PlayerInputDriver : MonoBehaviour
    {
        private VehicleController _vehicle;

        private void Awake()
        {
            _vehicle = GetComponent<VehicleController>();
        }

        private void Update()
        {
            if (Keyboard.current == null)
            {
                return;
            }

            float throttle = 0f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) throttle += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) throttle -= 1f;

            float steer = 0f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) steer -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) steer += 1f;

            Gamepad gamepad = Gamepad.current;
            if (gamepad != null)
            {
                Vector2 stick = gamepad.leftStick.ReadValue();
                throttle = Mathf.Clamp(throttle + stick.y, -1f, 1f);
                steer = Mathf.Clamp(steer + stick.x, -1f, 1f);
            }

            _vehicle.Drive(throttle, steer);
        }
    }
}
