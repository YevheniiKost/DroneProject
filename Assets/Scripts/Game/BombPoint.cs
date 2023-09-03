using UnityEngine;
using YT.Game.Drone;
using YT.Game.Projectiele;

namespace Game
{
    public class BombPoint : MonoBehaviour
    {
        [SerializeField] private Bomb _bombPrefab;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DroneBombModule drone))
            {
                drone.GetBomb(_bombPrefab);
            }
        }
    }
}