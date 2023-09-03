using System;
using UnityEngine;

namespace YT.Game.Drone
{
    public class Drone : MonoBehaviour
    {
        public const float MoveTrashold = 0.1f;

        public IDroneInput Input => _droneInput;
    
        public bool IsGrounded => _isGrounded;
        public bool IsSideRotating => Input.SideRotation > MoveTrashold || Input.SideRotation < -MoveTrashold;
        public DroneComponents Components { get; private set; }

        [SerializeField] private DroneInput _droneInput;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _groundCheckDistance = .2f;

        private bool _isGrounded;
        
        public void SetDroneComponents(DroneComponents components)
        {
            Components = components;
            foreach (var module in GetComponentsInParent<IDroneModule>())
            {
                module.SetDrone(this);
            }
        }

        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayerMask);
        }

        public void Damage()
        {
            Debug.Log("Drone damaged");
        }
    }

    public class DroneComponents
    {
        public DroneFollowTarget DroneFollowTarget;
        public GameObject ThirdPersonCamera;

    }
}
