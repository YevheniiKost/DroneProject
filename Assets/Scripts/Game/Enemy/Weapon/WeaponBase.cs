using UnityEngine;

namespace Enemy.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected Transform _bulletSpawnPoint;
        [SerializeField] private float _timeBetweenShots = 0.5f;

        private float _fireTimer;

        public virtual void Attack()
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer > 0)
                return;
            
            _fireTimer = _timeBetweenShots;
            Fire();
        }

        protected virtual void Fire()
        {
            
        }
    }
}