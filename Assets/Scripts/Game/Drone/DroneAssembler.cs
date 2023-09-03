using UnityEngine;

namespace YT.Game.Drone
{
    public class DroneAssembler : MonoBehaviour
    {
        [SerializeField] private DroneFollowTarget _droneFollowTargetPrefab;
        [SerializeField] private GameObject _thirdPersonCameraPrefab;
        [SerializeField] private Drone _drone;
        
        private void Awake()
        {
            DroneComponents components = new DroneComponents();
            var camera = Instantiate(_thirdPersonCameraPrefab, transform.position, transform.rotation);
            camera.name = "ThirdPersonCamera";
            var droneFollowTarget = Instantiate(_droneFollowTargetPrefab, transform.position, transform.rotation);
            droneFollowTarget.SetCamera(camera.transform, _drone);
            components.ThirdPersonCamera = camera;
            components.DroneFollowTarget = droneFollowTarget;
            _drone.SetDroneComponents(components);
            Destroy(this);
        }
    }
}