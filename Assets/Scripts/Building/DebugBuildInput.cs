using UnityEngine;
using UnityEngine.InputSystem;

namespace MadeToRace.Building
{
    /// <summary>
    /// PROTOTYPE-ONLY build input for the PrototypeBuild scene (PRD BLD-3:
    /// building must be learnable in the first match). Replaced by the real
    /// build UI (PRD UI-*). Keys: 1 = wheels, 2 = engine, 3 = reset.
    /// </summary>
    [RequireComponent(typeof(BuildPhaseController))]
    public sealed class DebugBuildInput : MonoBehaviour
    {
        private BuildPhaseController _build;

        private void Awake()
        {
            _build = GetComponent<BuildPhaseController>();
        }

        private void Update()
        {
            if (Keyboard.current == null)
            {
                return;
            }

            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                foreach (string slot in VehicleBuild.WheelSlots)
                {
                    _build.TryPlace(slot, PartType.Wheel);
                }
            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                _build.TryPlace(VehicleBuild.EngineSlot, PartType.Engine);
            }
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                _build.ResetBuild();
            }
        }
    }
}
