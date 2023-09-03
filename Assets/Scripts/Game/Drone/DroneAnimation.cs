using UnityEngine;

namespace YT.Game.Drone
{
    public class DroneAnimation : MonoBehaviour, IDroneModule
    {
        private const string IsFlyingTrigger = "IsFlying";
        
        
        [SerializeField] private Animator _animator;
        
        private Drone _drone;
        private int _isFlyingHash = Animator.StringToHash(IsFlyingTrigger);
        private bool _isFlying;
        
        public void SetDrone(Drone drone)
        {
           _drone = drone;
        }
        
        private void Update()
        {
            bool isFlying = !_drone.IsGrounded;
            if (_isFlying != isFlying)
            {
                _isFlying = isFlying;
                _animator.SetBool(_isFlyingHash, _isFlying);
            }
        }
    }
}