using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using MadeToRace.Vehicle;
using MadeToRace.Camera;

namespace MadeToRace.Editor
{
    /// <summary>
    /// Builds the M0 prototype scene: flat test course, drivable vehicle,
    /// follow camera, light. Run headlessly:
    ///   Unity -batchmode -executeMethod MadeToRace.Editor.PrototypeSceneBuilder.Build -quit
    /// </summary>
    public static class PrototypeSceneBuilder
    {
        private const string ScenePath = "Assets/Scenes/PrototypeDrive.unity";

        public static void Build()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // Directional light so the scene is visible in-editor.
            var lightGo = new GameObject("Directional Light");
            lightGo.AddComponent<Light>().type = LightType.Directional;
            lightGo.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            // Flat ground plane (large enough to drive around).
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0f, -0.5f, 0f);
            ground.transform.localScale = new Vector3(200f, 1f, 200f);

            // Prototype vehicle.
            var vehicle = CreatePrototypeVehicle();
            vehicle.transform.position = new Vector3(0f, 1f, 0f);

            // Follow camera.
            var cameraGo = new GameObject("Follow Camera");
            cameraGo.AddComponent<UnityEngine.Camera>();
            cameraGo.AddComponent<AudioListener>();
            var follow = cameraGo.AddComponent<CameraFollow>();
            follow.SetTarget(vehicle.transform);

            System.IO.Directory.CreateDirectory("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, ScenePath);
            Debug.Log($"[PrototypeSceneBuilder] Scene saved: {ScenePath}");
        }

        private static GameObject CreatePrototypeVehicle()
        {
            var root = new GameObject("Prototype Vehicle");

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

            var rb = root.AddComponent<Rigidbody>();
            rb.mass = 1.2f;
            rb.linearDamping = 0.05f;
            rb.angularDamping = 0.5f;

            root.AddComponent<VehicleController>();
            root.AddComponent<PlayerInputDriver>();
            return root;
        }
    }
}
