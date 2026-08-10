using System.Collections.Generic;
using System.Linq;

namespace MadeToRace.Building
{
    /// <summary>
    /// Build state for one vehicle: which parts are attached to which slots.
    /// Pure logic with no UnityEngine dependency so it runs in EditMode
    /// (see docs/ARCHITECTURE.md — Testing Approach).
    /// The chassis is the fixed base; engine and wheels are placeable.
    /// </summary>
    public sealed class VehicleBuild
    {
        public const string ChassisSlot = "chassis";
        public const string EngineSlot = "engine";
        public static readonly string[] WheelSlots = { "wheel-fl", "wheel-fr", "wheel-rl", "wheel-rr" };

        private readonly Dictionary<string, PartType> _slots = new Dictionary<string, PartType>
        {
            [ChassisSlot] = PartType.Chassis,
        };

        /// <summary>All currently attached parts (includes the base chassis).</summary>
        public IReadOnlyCollection<PartType> Parts => _slots.Values;

        public bool TryPlace(string slotId, PartType part)
        {
            if (part == PartType.Chassis || slotId == ChassisSlot) return false;
            if (_slots.ContainsKey(slotId) || !IsAllowedInSlot(slotId, part)) return false;

            _slots[slotId] = part;
            return true;
        }

        public bool TryRemove(string slotId)
        {
            return slotId != ChassisSlot && _slots.Remove(slotId);
        }

        /// <summary>Restores the clean state: base chassis only (PRD BLD-2).</summary>
        public void Reset()
        {
            _slots.Clear();
            _slots[ChassisSlot] = PartType.Chassis;
        }

        public bool IsRaceReady(BuildValidator validator) => validator.IsRaceReady(Parts);

        private static bool IsAllowedInSlot(string slotId, PartType part)
        {
            return slotId == EngineSlot ? part == PartType.Engine
                : WheelSlots.Contains(slotId) && part == PartType.Wheel;
        }
    }
}
