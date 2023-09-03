using UnityEngine;
using YT.Game.Projectiele;

namespace YT.Game.Drone
{
    public class DroneBombModule : MonoBehaviour, IDroneModule
    {
        [SerializeField] private Transform _bombSpawnPoint;
        [SerializeField] private float _bombDropForce = 10f;
        [SerializeField] private Transform _bombHolder;
        
        private Bomb _bomb;
        private Drone _drone;
        private bool _isBombOn;
        
        public void GetBomb(Bomb bombPrefab)
        {
            if (_isBombOn)
                return;
            
            _isBombOn = true;
            _bomb = Instantiate(bombPrefab);
            _bomb.SetAsChild(_bombHolder);
        }

        public void SetDrone(Drone drone)
        {
            _drone = drone;
            _drone.Input.OnBombDropped += OnBombDropped;
        }

        private void OnBombDropped()
        {
            if (_drone.Input.CurrentViewMode == ViewMode.FirstPerson && _isBombOn)
            {
                DropBomb();
            }
        }

        private void DropBomb()
        {
            _bomb.Drop(_bombDropForce);
            _isBombOn = false;
            _bomb = null;
        }
    }
}