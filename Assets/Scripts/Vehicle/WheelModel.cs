using UnityEngine;

namespace MadeToRace.Vehicle
{
    /// <summary>
    /// One wheel: raycast suspension (spring + damper). Normal load comes from
    /// spring compression — weight transfer under accel/brake/cornering emerges
    /// from the rigidbody's motion, not lookup tables (docs/DECISIONS.md).
    /// Contact friction (grip limits) is applied by VehicleController, which
    /// has the throttle/steer context.
    /// </summary>
    public sealed class WheelModel : MonoBehaviour
    {
        [SerializeField] private float restLength = 0.45f;
        [SerializeField] private float springStiffness = 4000f; // N/m (kart-ish)
        [SerializeField] private float damper = 250f;           // N·s/m
        [SerializeField] private float wheelRadius = 0.35f;     // matches visual wheels

        private Rigidbody _body;
        private float _normalLoad;
        private Vector3 _lastContactPoint;

        /// <summary>Current normal force on this wheel (N). 0 when airborne.</summary>
        public float NormalLoad => _normalLoad;

        /// <summary>True when the suspension ray hit something this step.</summary>
        public bool OnGround => _normalLoad > 1f;

        /// <summary>World position where the tire meets the road.</summary>
        public Vector3 ContactPoint => _lastContactPoint;

        public void Attach(Rigidbody body) => _body = body;

        private void FixedUpdate()
        {
            if (_body == null)
            {
                return;
            }

            Vector3 origin = transform.position;
            if (Physics.Raycast(origin, -transform.up, out RaycastHit hit, restLength + wheelRadius))
            {
                _lastContactPoint = hit.point;

                // Spring: compression beyond rest; damper: suspension velocity.
                float compression = restLength - (hit.distance - wheelRadius);
                float suspensionSpeed = Vector3.Dot(_body.GetPointVelocity(origin), -transform.up);
                float springForce = springStiffness * compression;
                float dampingForce = damper * suspensionSpeed;
                float total = Mathf.Max(0f, springForce - dampingForce);

                _body.AddForceAtPosition(transform.up * total, origin, ForceMode.Force);
                _normalLoad = total;
            }
            else
            {
                _normalLoad = 0f;
            }
        }
    }
}
