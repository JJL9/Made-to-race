namespace MadeToRace.Building
{
    /// <summary>
    /// Vehicle part categories known to the build system. Grows as the part
    /// catalog expands. MVP minimum vehicle: chassis + wheels + engine.
    /// </summary>
    public enum PartType
    {
        Chassis,
        Wheel,
        Engine,
    }
}
