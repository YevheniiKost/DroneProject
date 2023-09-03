using System;
using UnityEngine;
using YT.Game.Drone;

namespace Enemy
{
    public class EnemyDroneDetector : MonoBehaviour
    {
        public event Action<Drone> OnDroneDetected;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Drone>(out var drone))
            {
                OnDroneDetected?.Invoke(drone);
            }
                
        }
    }
}