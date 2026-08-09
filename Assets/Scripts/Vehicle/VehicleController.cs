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
        [SerializeField, Min(0f)] private float accelerationForce = 1500f;
        [SerializeField, Min(0f)] private float brakeForce = 2200f;
        [SerializeField, Min(0f)] private float turnTorque = 8f;
        [SerializeField, Min(0f)] private float maxSpeed = 30f;
        [SerializeField, Min(0f)] private float lateralGrip = 6f;

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

            // Damp lateral velocity so the vehicle tracks forward (arcade grip).
            Vector3 lateral = Vector3.Project(velocity, transform.right);
            _body.AddForce(-lateral * lateralGrip, ForceMode.Acceleration);
        }
    }
}
