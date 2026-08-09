using UnityEngine;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// Drives the assembled vehicle: reads input and applies forces to the
    /// vehicle's Rigidbody. Owns no building or race rules (see docs/ARCHITECTURE.md).
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class VehicleController : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float accelerationForce = 1500f;
        [SerializeField, Min(0f)] private float turnTorque = 8f;
        [SerializeField, Min(0f)] private float maxSpeed = 30f;

        private Rigidbody _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Applies player input to the vehicle. Throttle and steer are in
        /// the range [-1, 1]. Starting stub — tuned during the P0 prototype.
        /// </summary>
        public void Drive(float throttle, float steer)
        {
            Vector3 velocity = _body.linearVelocity;
            float forwardSpeed = Vector3.Dot(velocity, transform.forward);

            if (forwardSpeed < maxSpeed)
            {
                _body.AddForce(transform.forward * (throttle * accelerationForce));
            }

            // Steering takes effect as the vehicle gains speed.
            float turn = steer * turnTorque * Mathf.Clamp01(Mathf.Abs(forwardSpeed) / maxSpeed);
            _body.AddTorque(transform.up * turn, ForceMode.Acceleration);
        }
    }
}
