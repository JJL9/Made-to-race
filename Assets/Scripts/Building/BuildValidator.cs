using System.Collections.Generic;
using System.Linq;

namespace MadeToRace.Building
{
    /// <summary>
    /// Decides whether an assembled vehicle is race-ready. Pure logic with no
    /// UnityEngine dependency so it can be tested in EditMode
    /// (see docs/ARCHITECTURE.md — Testing Approach).
    /// </summary>
    public sealed class BuildValidator
    {
        private readonly int _minimumWheels;

        public BuildValidator(int minimumWheels = 1)
        {
            _minimumWheels = minimumWheels;
        }

        public bool IsRaceReady(IReadOnlyCollection<PartType> parts)
        {
            return parts.Contains(PartType.Chassis)
                && parts.Contains(PartType.Engine)
                && parts.Count(part => part == PartType.Wheel) >= _minimumWheels;
        }
    }
}
