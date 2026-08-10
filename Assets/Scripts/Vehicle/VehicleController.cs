using System.Collections.Generic;
using UnityEngine;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// Drives the assembled vehicle through its four wheel models: raycast
    /// suspension + friction-limited contact (friction circle). Engine force
    /// follows a real power curve (F = P/v) traction-capped by driven-wheel
    /// grip; drag ∝ v²; no artificial speed cap and no yaw clamping — sliding,
    /// understeer, and oversteer emerge from grip limits (DECISIONS.md).
    /// Configured from part specs via AssembledVehicle (Building.PartSpecs).
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class VehicleController : MonoBehaviour
    {
        // Default spec = kart class (PartSpecs). BuildPhaseController
        // overrides via Configure() as parts are attached.
        [SerializeField] private float massKg = 177f;
        [SerializeField] private float powerWatts = 15000f;
        [SerializeField] private float frictionCoeff = 1.0f;
        [SerializeField] private float dragCoeff = 0.7f;
        [SerializeField] private float frontalArea = 0.9f;
        [SerializeField] private float steerBias = 0.75f;  // rear wheels get less steer demand → mild understeer
        [SerializeField] private float steerSpeedGain = 8f; // m/s of forward speed to reach full steering authority

        private Rigidbody _body;
        private bool _powered = true;
        private readonly List<WheelModel> _wheels = new List<WheelModel>(4);

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _body.mass = massKg;
            SpawnWheels();
        }

        /// <summary>Whether the vehicle has a power source (set by the build phase).</summary>
        public void SetPowered(bool powered) => _powered = powered;

        /// <summary>Applies part-derived numbers to the physics model.</summary>
        public void Configure(AssembledVehicle spec)
        {
            massKg = spec.MassKg;
            powerWatts = spec.PowerWatts;
            frictionCoeff = spec.FrictionCoeff;
            dragCoeff = spec.DragCoeff;
            frontalArea = spec.FrontalArea;
            _body.mass = massKg;
        }

        /// <summary>
        /// Applies player input (throttle, steer ∈ [-1, 1]). Call from
        /// FixedUpdate (input driver or tests).
        /// </summary>
        public void Drive(float throttle, float steer)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            Vector3 velocity = _body.linearVelocity;
            float speed = Vector3.Dot(velocity, forward);
            float speedAbs = Mathf.Abs(speed);

            // --- Grip from live wheel loads (weight transfer emerges) ---
            float rearGrip = 0f;
            foreach (WheelModel wheel in _wheels)
            {
                rearGrip += frictionCoeff * wheel.NormalLoad;
            }
            rearGrip *= 0.5f; // driven wheels = rear axle share (RWD kart)

            // --- Drive force: power curve, traction-limited ---
            float drive = _powered && throttle > 0f
                ? VehiclePhysics.DriveForce(powerWatts, speedAbs, rearGrip) * throttle
                : 0f;

            // --- Drag ∝ v² at the body ---
            float drag = VehiclePhysics.DragForce(speedAbs, dragCoeff, frontalArea);

            // --- Per-wheel friction circle: longitudinal + lateral demand ---
            float steerAmount = steer * Mathf.Clamp01(speedAbs / steerSpeedGain);
            float forwardForce = drive - drag * Mathf.Sign(speed);

            Vector3 lateralVelocity = Vector3.Project(velocity, right);

            for (int i = 0; i < _wheels.Count; i++)
            {
                WheelModel wheel = _wheels[i];
                float load = wheel.NormalLoad;
                if (load <= 0f)
                {
                    continue;
                }

                float grip = frictionCoeff * load;

                // Longitudinal: drive on rear wheels; brake on all wheels.
                float longitudinal = throttle < 0f
                    ? -Mathf.Sign(speed) * grip * -throttle          // brake = grip-limited (ABS-ish)
                    : (i >= 2 ? forwardForce * 0.5f : 0f);           // rear wheels split the drive

                // Lateral: tire resists sliding (self-aligning) + steer demand
                // (front-biased, along the wheel axis → yaw moment).
                Vector3 slideResist = -lateralVelocity.normalized * grip;
                Vector3 steerForce = right * steerAmount * grip * (i < 2 ? 1f : steerBias);
                Vector3 lateral = slideResist + steerForce;

                // Friction circle: cap combined demand at μ × load.
                float lateralMagnitude = lateral.magnitude;
                float combined = Mathf.Sqrt(longitudinal * longitudinal + lateralMagnitude * lateralMagnitude);
                if (combined > grip)
                {
                    float scale = grip / combined;
                    longitudinal *= scale;
                    lateral *= scale;
                }

                Vector3 force = forward * longitudinal + lateral;
                _body.AddForceAtPosition(force, wheel.ContactPoint, ForceMode.Force);
            }
        }

        private void SpawnWheels()
        {
            // Wheel slot layout matching the prototype vehicle (2×2 rectangle).
            Vector3[] localPositions =
            {
                new Vector3(-1.1f, -0.4f, 1.4f),
                new Vector3(1.1f, -0.4f, 1.4f),
                new Vector3(-1.1f, -0.4f, -1.4f),
                new Vector3(1.1f, -0.4f, -1.4f),
            };

            for (int i = 0; i < localPositions.Length; i++)
            {
                var wheelGo = new GameObject("WheelPhysics" + i);
                wheelGo.transform.SetParent(transform);
                wheelGo.transform.localPosition = localPositions[i];
                WheelModel wheel = wheelGo.AddComponent<WheelModel>();
                wheel.Attach(_body);
                _wheels.Add(wheel);
            }
        }
    }
}
