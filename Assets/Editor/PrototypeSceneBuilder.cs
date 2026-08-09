using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using MadeToRace.Building;
using MadeToRace.Vehicle;
using MadeToRace.Camera;

namespace MadeToRace.Editor
{
    /// <summary>
    /// Builds the M0 prototype scenes. Run headlessly:
    ///   Unity -batchmode -executeMethod MadeToRace.Editor.PrototypeSceneBuilder.Build -quit
    /// Scenes:
    ///   Assets/Scenes/PrototypeDrive.unity — ready-built vehicle, drive immediately.
    ///   Assets/Scenes/PrototypeBuild.unity — chassis only; 1 = wheels, 2 = engine,
    ///   3 = reset, then drive (M0-3, PRD BLD-1..BLD-3).
    /// </summary>
    public static class PrototypeSceneBuilder
    {
        private const string ScenesDirectory = "Assets/Scenes";

        public static void Build()
        {
            BuildDriveScene();
            BuildBuildScene();
        }

        private static void BuildDriveScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            AddGroundAndLight();

            var vehicle = CreatePrototypeVehicle();
            vehicle.transform.position = new Vector3(0f, 1f, 0f);

            AddFollowCamera(vehicle);

            System.IO.Directory.CreateDirectory(ScenesDirectory);
            EditorSceneManager.SaveScene(scene, ScenesDirectory + "/PrototypeDrive.unity");
            Debug.Log("[PrototypeSceneBuilder] Saved Assets/Scenes/PrototypeDrive.unity");
        }

        private static void BuildBuildScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            AddGroundAndLight();

            var vehicle = CreateBuildableVehicle();
            vehicle.transform.position = new Vector3(0f, 1f, 0f);

            AddFollowCamera(vehicle);
            AddBuildHintText();

            System.IO.Directory.CreateDirectory(ScenesDirectory);
            EditorSceneManager.SaveScene(scene, ScenesDirectory + "/PrototypeBuild.unity");
            Debug.Log("[PrototypeSceneBuilder] Saved Assets/Scenes/PrototypeBuild.unity");
        }

        private static void AddGroundAndLight()
        {
            var lightGo = new GameObject("Directional Light");
            lightGo.AddComponent<Light>().type = LightType.Directional;
            lightGo.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0f, -0.5f, 0f);
            ground.transform.localScale = new Vector3(200f, 1f, 200f);
        }

        private static void AddFollowCamera(GameObject target)
        {
            var cameraGo = new GameObject("Follow Camera");
            cameraGo.AddComponent<UnityEngine.Camera>();
            cameraGo.AddComponent<AudioListener>();
            var follow = cameraGo.AddComponent<CameraFollow>();
            follow.SetTarget(target.transform);
        }

        private static void AddBuildHintText()
        {
            var canvasGo = new GameObject("Build Hint");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var textGo = new GameObject("Hint Text");
            textGo.transform.SetParent(canvasGo.transform);
            var text = textGo.AddComponent<UnityEngine.UI.Text>();
            text.text = "BUILD:  1 = wheels   2 = engine   3 = reset   |   then drive with WASD";
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 24;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;

            var rect = text.rectTransform;
            rect.anchorMin = new Vector2(0f, 0.9f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        private static GameObject CreatePrototypeVehicle()
        {
            var root = new GameObject("Prototype Vehicle");
            AddBodyAndWheels(root);
            AddVehicleDrivingComponents(root);
            return root;
        }

        private static GameObject CreateBuildableVehicle()
        {
            var root = new GameObject("Buildable Vehicle");
            AddBodyAndWheels(root);
            AddVehicleDrivingComponents(root);
            root.AddComponent<BuildPhaseController>();
            root.AddComponent<DebugBuildInput>();
            return root;
        }

        private static void AddBodyAndWheels(GameObject root)
        {
            var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            body.name = "Body";
            body.transform.SetParent(root.transform);
            body.transform.localScale = new Vector3(2f, 0.8f, 4f);

            // Wheels are visual-only in the prototype; the body collider is
            // what contacts the ground (physics is body-driven, PHY-1..4).
            for (int i = 0; i < 4; i++)
            {
                var wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                wheel.name = "Wheel " + (i + 1);
                wheel.transform.SetParent(root.transform);
                float side = (i % 2 == 0) ? -1f : 1f;
                float front = (i < 2) ? 1.4f : -1.4f;
                wheel.transform.localPosition = new Vector3(side * 1.1f, -0.45f, front);
                wheel.transform.localScale = new Vector3(0.7f, 0.35f, 0.7f);
                wheel.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                Object.DestroyImmediate(wheel.GetComponent<Collider>());
            }
        }

        private static void AddVehicleDrivingComponents(GameObject root)
        {
            var rb = root.AddComponent<Rigidbody>();
            rb.mass = 1.2f;
            rb.linearDamping = 0.05f;
            rb.angularDamping = 0.5f;

            root.AddComponent<VehicleController>();
            root.AddComponent<PlayerInputDriver>();
        }
    }
}
