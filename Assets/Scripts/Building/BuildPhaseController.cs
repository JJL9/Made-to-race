using System.Collections.Generic;
using System.Linq;
using MadeToRace.Vehicle;
using UnityEngine;

namespace MadeToRace.Building
{
    /// <summary>
    /// Build-phase gameplay on a vehicle: owns the VehicleBuild, spawns visual
    /// parts onto the chassis, and applies build consequences to the vehicle
    /// (an engine is required for power — PRD BLD-1). Build UI is a later task
    /// (PRD UI-*); this exposes the operations the PlayMode tests and the UI
    /// will call.
    /// </summary>
    [RequireComponent(typeof(VehicleController))]
    public sealed class BuildPhaseController : MonoBehaviour
    {
        private readonly VehicleBuild _build = new VehicleBuild();
        private readonly BuildValidator _validator = new BuildValidator();
        private readonly Dictionary<string, GameObject> _partViews = new Dictionary<string, GameObject>();

        private VehicleController _vehicle;

        public VehicleBuild Build => _build;

        private void Awake()
        {
            _vehicle = GetComponent<VehicleController>();
            ApplyBuildToVehicle(); // fresh build: chassis only — no power, no grip
        }

        public bool TryPlace(string slotId, PartType part)
        {
            if (!_build.TryPlace(slotId, part)) return false;

            SpawnPartView(slotId, part);
            ApplyBuildToVehicle();
            return true;
        }

        public bool TryRemove(string slotId)
        {
            if (!_build.TryRemove(slotId)) return false;

            DestroyPartView(slotId);
            ApplyBuildToVehicle();
            return true;
        }

        /// <summary>Restores the clean state: base chassis only (PRD BLD-2).</summary>
        public void ResetBuild()
        {
            _build.Reset();
            foreach (GameObject view in _partViews.Values)
            {
                Destroy(view);
            }
            _partViews.Clear();
            ApplyBuildToVehicle();
        }

        public bool IsRaceReady() => _build.IsRaceReady(_validator);

        /// <summary>
        /// Pushes part-derived numbers into the vehicle physics: power from the
        /// engine, grip from wheels (wheel count scales traction — a car with
        /// fewer wheels has less grip, BLD-4), mass from chassis + parts.
        /// </summary>
        private void ApplyBuildToVehicle()
        {
            int wheelCount = _build.Parts.Count(part => part == PartType.Wheel);
            bool hasEngine = _build.Parts.Contains(PartType.Engine);

            ChassisSpec chassis = PartSpecs.KartChassis;
            EngineSpec engine = PartSpecs.KartEngine;
            WheelSpec wheel = PartSpecs.StreetWheel;

            AssembledVehicle spec = VehicleAssembly.Combine(
                chassis.MassKg, chassis.DragCoeff, chassis.FrontalArea,
                chassis.CenterHeight, chassis.Wheelbase, chassis.TrackWidth, chassis.FrontWeightRatio,
                hasEngine ? engine.MassKg : 0f,
                hasEngine ? engine.PowerWatts : 0f,
                wheel.FrictionCoeff * (wheelCount / 4f), // grip scales with wheel count
                wheelCount);

            _vehicle.Configure(spec);
        }

        private void SpawnPartView(string slotId, PartType part)
        {
            GameObject view;
            if (slotId == VehicleBuild.EngineSlot)
            {
                view = GameObject.CreatePrimitive(PrimitiveType.Cube);
                view.name = "Engine Part";
                view.transform.SetParent(transform);
                view.transform.localPosition = new Vector3(0f, 0.25f, -1.2f);
                view.transform.localScale = new Vector3(1.2f, 0.6f, 0.8f);
                SetColor(view, new Color(0.9f, 0.2f, 0.2f));
            }
            else
            {
                view = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                view.name = "Wheel Part (" + slotId + ")";
                view.transform.SetParent(transform);

                // Mirror the prototype vehicle's wheel layout.
                bool front = slotId.EndsWith("fl") || slotId.EndsWith("fr");
                bool left = slotId.EndsWith("fl") || slotId.EndsWith("rl");
                view.transform.localPosition = new Vector3(left ? -1.1f : 1.1f, -0.05f, front ? 1.4f : -1.4f);
                view.transform.localScale = new Vector3(0.7f, 0.35f, 0.7f);
                view.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                SetColor(view, new Color(0.15f, 0.15f, 0.15f));
            }

            // Part views are visual only — colliders on them overlap the body
            // and ground (engine sits inside the body volume, wheels below the
            // ground surface) and would fire depenetration forces. The raycast
            // WheelModels + body carry all contact.
            Object.Destroy(view.GetComponent<Collider>());

            _partViews[slotId] = view;
        }

        private void DestroyPartView(string slotId)
        {
            if (_partViews.TryGetValue(slotId, out GameObject view) && view != null)
            {
                Destroy(view);
                _partViews.Remove(slotId);
            }
        }

        private static void SetColor(GameObject go, Color color)
        {
            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }
}
