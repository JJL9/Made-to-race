namespace MadeToRace.Race
{
    /// <summary>
    /// Minimal race state machine: Build -> Countdown -> Racing -> Finished.
    /// Pure logic, testable in EditMode. Reset returns to Build.
    /// </summary>
    public sealed class RaceState
    {
        public RacePhase Phase { get; private set; } = RacePhase.Build;

        /// <summary>Advances one phase. Returns false once Finished.</summary>
        public bool TryAdvance()
        {
            if (Phase == RacePhase.Finished)
            {
                return false;
            }

            Phase++;
            return true;
        }

        public void Reset()
        {
            Phase = RacePhase.Build;
        }
    }
}
