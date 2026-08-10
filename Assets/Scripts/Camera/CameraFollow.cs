using UnityEngine;

namespace MadeToRace.Camera
{
    /// <summary>Simple follow camera for the prototype test course.</summary>
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -9f);
        [SerializeField, Min(0f)] private float smoothTime = 0.15f;

        private Vector3 _velocity;

        public void SetTarget(Transform followTarget)
        {
            target = followTarget;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 desired = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref _velocity, smoothTime);
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}
