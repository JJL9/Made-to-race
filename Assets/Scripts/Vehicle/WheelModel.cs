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
        [SerializeField] private float damper = 750f;           // N·s/m (~0.9 critical per wheel)
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

            // Raycast the suspension along the GROUND NORMAL (world down), not
            // the body axis: the tire's support force acts perpendicular to the
            // road. Body-axis rays/forces on a pitched chassis resolve into a
            // horizontal creep (the 45/55 stance's 0.45° tilt pushed a
            // chassis-only build at ~0.05 m/s) and feed tilt amplification.
            // Skipping the vehicle's own colliders (body cube, part views) —
            // only external surfaces load the tire.
            RaycastHit hit = default;
            bool grounded = false;
            foreach (RaycastHit candidate in Physics.RaycastAll(origin, Vector3.down, restLength + wheelRadius))
            {
                if (candidate.collider.transform.IsChildOf(_body.transform))
                {
                    continue;
                }
                hit = candidate;
                grounded = true;
                break;
            }

            if (grounded)
            {
                _lastContactPoint = hit.point;

                // Spring: compression beyond rest; damper: opposes the compression
                // rate (F = k·c + b·ċ — adds force on the downswing, subtracts on
                // the upswing; a flipped sign pumps energy into the bounce).
                // Compression is clamped to the spring's own travel: a wheel
                // spawned embedded in the ground must not turn the suspension
                // into a catapult (this launched the prototype scene at spawn).
                float compression = Mathf.Clamp(restLength - (hit.distance - wheelRadius), 0f, restLength);
                float compressionSpeed = Vector3.Dot(_body.GetPointVelocity(origin), Vector3.down);
                float springForce = springStiffness * compression;
                float dampingForce = damper * compressionSpeed;
                float total = Mathf.Max(0f, springForce + dampingForce);

                // Apply the ground reaction AT THE CONTACT PATCH, not the wheel
                // origin: the tire's support force acts where the tire meets the
                // road. With the CoM at kart CoG height (below the spring attach
                // plane), applying at the origin makes the chassis an inverted
                // pendulum — a landing bounce tips it and the springs amplify the
                // tilt until it flops and skitters (chassis-only builds hit
                // ~11 m/s with no input). At the contact patch the support is
                // below the CoM → restoring → stable at any CoM height.
                _body.AddForceAtPosition(Vector3.up * total, hit.point, ForceMode.Force);
                _normalLoad = total;
            }
            else
            {
                _normalLoad = 0f;
            }
        }
    }
}
