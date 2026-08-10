namespace MadeToRace.Building
{
    /// <summary>
    /// Real-world-derived part specs, kart class. Reference data
    /// (docs/DECISIONS.md — Vehicle Physics Complexity):
    /// kart ~150–200 kg with driver, ~10–30 kW, μ ≈ 1.0 dry asphalt,
    /// Cd ≈ 0.7 boxy kart, frontal area ≈ 0.9 m², CoG ≈ 0.3 m.
    /// These are plausible engineering numbers, not a full sim model.
    /// </summary>
    public sealed class EngineSpec
    {
        public float PowerWatts;
        public float MassKg;
    }

    public sealed class WheelSpec
    {
        public float FrictionCoeff; // μ, dry asphalt: street ~0.9–1.0, semi-slick ~1.1, slick ~1.3
        public float MassKg;
    }

    public sealed class ChassisSpec
    {
        public float MassKg;          // includes driver (60 kg)
        public float DragCoeff;       // Cd
        public float FrontalArea;     // m²
        public float CenterHeight;    // CoG height, m
        public float Wheelbase;       // front–rear axle distance, m
        public float TrackWidth;      // left–right wheel distance, m
        public float FrontWeightRatio; // 0..1 fraction of weight on the front axle
    }

    public static class PartSpecs
    {
        public const float DriverMassKg = 60f;

        public static ChassisSpec KartChassis => new ChassisSpec
        {
            MassKg = 80f + DriverMassKg,
            DragCoeff = 0.7f,
            FrontalArea = 0.9f,
            CenterHeight = 0.3f,
            Wheelbase = 1.4f,
            TrackWidth = 1.2f,
            FrontWeightRatio = 0.45f, // 45/55 front/rear (mid-engine kart)
        };

        public static EngineSpec KartEngine => new EngineSpec
        {
            PowerWatts = 15000f, // 15 kW ≈ 20 hp
            MassKg = 25f,
        };

        public static WheelSpec StreetWheel => new WheelSpec
        {
            FrictionCoeff = 1.0f,
            MassKg = 3f,
        };
    }
}
