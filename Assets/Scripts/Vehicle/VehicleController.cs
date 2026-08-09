using UnityEngine;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// Drives the assembled vehicle: applies throttle/steering forces to the
    /// vehicle's Rigidbody. Owns no building or race rules (see docs/ARCHITECTURE.md).
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class VehicleController : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float accelerationForce = 15f;   // ~12.5 m/s^2 (~1.3g) on mass 1.2
        [SerializeField, Min(0f)] private float brakeForce = 20f;          // ~16.7 m/s^2 (~1.7g) — brakes stronger than engine
        [SerializeField, Min(0f)] private float turnTorque = 3f;           // yaw acceleration at speed (rad/s^2)
        [SerializeField, Min(0f)] private float maxSpeed = 18f;            // ~65 km/h, kart territory for a 200m course
        [SerializeField, Min(0f)] private float maxYawRate = 1.5f;         // ~86 deg/s — clamps yaw so the car can't spin out
        [SerializeField, Min(0f)] private float lateralGrip = 12f;         // strong lateral damping — grip dominates power

        private Rigidbody _body;
        private bool _powered = true; // PrototypeDrive starts drivable; build flow controls this.

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Whether the vehicle has a power source. The build phase calls this
        /// as parts are attached/removed (see BuildPhaseController).
        /// </summary>
        public void SetPowered(bool powered) => _powered = powered;

        /// <summary>
        /// Applies player input. Throttle and steer are in [-1, 1];
        /// negative throttle brakes (or reverses once nearly stopped).
        /// Without power, throttle is ignored but steering and braking still work.
        /// </summary>
        public void Drive(float throttle, float steer)
        {
            Vector3 velocity = _body.linearVelocity;
            float forwardSpeed = Vector3.Dot(velocity, transform.forward);

            if (_powered && throttle > 0f && forwardSpeed < maxSpeed)
            {
                _body.AddForce(transform.forward * (throttle * accelerationForce));
            }
            else if (throttle < 0f)
            {
                _body.AddForce(-transform.forward * (brakeForce * -throttle));
            }

            // Steering takes effect as the vehicle gains speed.
            float turn = steer * turnTorque * Mathf.Clamp01(Mathf.Abs(forwardSpeed) / maxSpeed);
            _body.AddTorque(transform.up * turn, ForceMode.Acceleration);

            // Clamp yaw rate so the vehicle can't spin out (arcade grip;
            // real cars self-stabilize via tire slip. This is the stand-in
            // until real wheel physics lands in a later milestone).
            Vector3 angularVelocity = _body.angularVelocity;
            angularVelocity.y = Mathf.Clamp(angularVelocity.y, -maxYawRate, maxYawRate);
            _body.angularVelocity = angularVelocity;

            // Damp lateral velocity so the vehicle tracks forward (arcade grip).
            Vector3 lateral = Vector3.Project(velocity, transform.right);
            _body.AddForce(-lateral * lateralGrip, ForceMode.Acceleration);
        }
    }
}
