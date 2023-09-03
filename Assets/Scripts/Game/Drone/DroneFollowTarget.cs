using System;
using UnityEngine;

namespace YT.Game.Drone
{
    public class DroneFollowTarget : MonoBehaviour
    {
        [SerializeField] private Drone _target;
        [SerializeField] private Transform _thirdPersonCamera;

        public void SetCamera(Transform camera, Drone target)
        {
            _thirdPersonCamera = camera;
            _target = target;
        }

        private void LateUpdate()
        {
            transform.position = _target.transform.position;
            if (_target.IsSideRotating)
                transform.rotation = _target.transform.rotation;
            else
                transform.rotation = Quaternion.LookRotation(transform.position - _thirdPersonCamera.position);
        }
    }
}