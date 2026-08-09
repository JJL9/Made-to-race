using System;
using UnityEngine;

namespace MadeToRace.Race
{
    /// <summary>
    /// Reports when a vehicle crosses the finish line. Place this on a trigger
    /// volume spanning the finish line (see docs/ARCHITECTURE.md).
    /// </summary>
    public sealed class FinishDetector : MonoBehaviour
    {
        public event Action Finished;

        private void OnTriggerEnter(Collider other)
        {
            // Only a physical vehicle (attached Rigidbody) counts as crossing.
            if (other.attachedRigidbody != null)
            {
                Finished?.Invoke();
            }
        }
    }
}
