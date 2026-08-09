using System;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// Pure vehicle physics math — no UnityEngine dependency (runs in EditMode
    /// and standalone dotnet verification). Models real car systems at simcade
    /// depth per docs/DECISIONS.md — Vehicle Physics Complexity.
    /// </summary>
    public static class VehiclePhysics
    {
        public const float AirDensity = 1.225f; // kg/m^3 at sea level
        public const float Gravity = 9.81f;     // m/s^2

        /// <summary>
        /// Drive force from engine power (kart CVT approximation: F = P/v),
        /// capped by the traction limit of the driven wheels.
        /// </summary>
        public static float DriveForce(float powerWatts, float speed, float tractionLimit)
        {
            if (speed <= 0.1f)
            {
                return tractionLimit; // launch: power curve is infinite at v=0 — grip-limited
            }
            return Math.Min(powerWatts / speed, tractionLimit);
        }

        /// <summary>Aerodynamic drag: 0.5 ρ Cd A v².</summary>
        public static float DragForce(float speed, float dragCoeff, float frontalArea)
        {
            return 0.5f * AirDensity * dragCoeff * frontalArea * speed * speed;
        }

        /// <summary>Aerodynamic downforce: 0.5 ρ Cl A v².</summary>
        public static float Downforce(float speed, float downforceCoeff, float frontalArea)
        {
            return 0.5f * AirDensity * downforceCoeff * frontalArea * speed * speed;
        }

        /// <summary>Top speed where drive force equals drag: v = (2P / ρCdA)^(1/3).</summary>
        public static float TopSpeed(float powerWatts, float dragCoeff, float frontalArea)
        {
            float denominator = AirDensity * dragCoeff * frontalArea;
            if (denominator <= 0f)
            {
                return float.PositiveInfinity;
            }
            return MathF.Pow(2f * powerWatts / denominator, 1f / 3f);
        }

        /// <summary>Grip limit of a tire: μ × normal load.</summary>
        public static float GripLimit(float frictionCoeff, float normalLoad)
        {
            return frictionCoeff * normalLoad;
        }

        /// <summary>
        /// Longitudinal weight transfer under acceleration: m·a·h / wheelbase.
        /// Positive a (accelerating) shifts load to the rear axle.
        /// </summary>
        public static float LongitudinalTransfer(float totalMass, float acceleration, float centerHeight, float wheelbase)
        {
            return totalMass * acceleration * centerHeight / wheelbase;
        }

        /// <summary>
        /// Lateral weight transfer in a corner: m·a_y·h / track.
        /// Load shifts to the outside wheels.
        /// </summary>
        public static float LateralTransfer(float totalMass, float lateralAccel, float centerHeight, float trackWidth)
        {
            return totalMass * lateralAccel * centerHeight / trackWidth;
        }
    }

    /// <summary>
    /// Physics-relevant numbers of a fully assembled vehicle, combined from
    /// part specs (see Building.PartSpecs). Feed into VehicleController.
    /// </summary>
    public sealed class AssembledVehicle
    {
        public float MassKg;
        public float PowerWatts;
        public float FrictionCoeff;
        public float DragCoeff;
        public float FrontalArea;
        public float CenterHeight;
        public float Wheelbase;
        public float TrackWidth;
        public float FrontWeightRatio;
    }

    /// <summary>
    /// Combines chassis + engine + wheels into an AssembledVehicle.
    /// Pure logic — no UnityEngine (EditMode-testable).
    /// </summary>
    public static class VehicleAssembly
    {
        public static AssembledVehicle Combine(
            float chassisMassKg, float chassisDragCoeff, float chassisFrontalArea,
            float centerHeight, float wheelbase, float trackWidth, float frontWeightRatio,
            float engineMassKg, float enginePowerWatts, float wheelFrictionCoeff, int wheelCount)
        {
            return new AssembledVehicle
            {
                MassKg = chassisMassKg + engineMassKg + wheelCount * 3f, // per-wheel mass, see PartSpecs
                PowerWatts = enginePowerWatts,
                FrictionCoeff = wheelFrictionCoeff,
                DragCoeff = chassisDragCoeff,
                FrontalArea = chassisFrontalArea,
                CenterHeight = centerHeight,
                Wheelbase = wheelbase,
                TrackWidth = trackWidth,
                FrontWeightRatio = frontWeightRatio,
            };
        }
    }
}
