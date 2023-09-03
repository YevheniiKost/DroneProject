using System;
using Cinemachine;
using UnityEngine;

namespace YT.Game.Drone
{
    public class DroneViewModule : MonoBehaviour, IDroneModule
    {
        [SerializeField] private CinemachineVirtualCameraBase _firstPersonCamera;
        [SerializeField] private CinemachineVirtualCameraBase _thirdPersonCamera;

        private Drone _drone;
        
        public void SetDrone(Drone drone)
        {
            _drone = drone;
            _drone.Input.OnViewModeChanged += OnViewModeChanged;
            SetThirdPersonCamera(_drone.Components.ThirdPersonCamera, _drone.Components.DroneFollowTarget);
        }

        private void OnViewModeChanged(ViewMode mode)
        {
            if (mode == ViewMode.FirstPerson)
            {
                _firstPersonCamera.Priority = 1;
                _thirdPersonCamera.Priority = 0;
            }
            else
            {
                _thirdPersonCamera.Priority = 1;
                _firstPersonCamera.Priority = 0;
            }
        }
        
        private void SetThirdPersonCamera(GameObject camera, DroneFollowTarget droneFollowTarget)
        {
            if (camera.TryGetComponent<CinemachineVirtualCameraBase>(out var virtualCamera))
            {
                _thirdPersonCamera = virtualCamera;
                _thirdPersonCamera.Follow = droneFollowTarget.transform;
                _thirdPersonCamera.LookAt = droneFollowTarget.transform;

            }
            else
                throw new ArgumentException("Camera must contains CinemachineVirtualCameraBase", nameof(camera));
        }
    }
}