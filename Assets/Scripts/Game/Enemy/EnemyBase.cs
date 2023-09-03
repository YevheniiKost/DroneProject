using System;
using UnityEngine;

namespace Enemy
{
    public interface IExplodable
    {
        void Explode(Vector3 position, float force, float radius);
    }
    
    
    public class EnemyBase : MonoBehaviour
    {
        public Transform SpawnPoint { get; private set; }
        
        
        [SerializeField] private int _maxHealth = 100;
        
        private int _health;
        protected bool IsDead;

        public virtual void Init(Transform spawnPoint)
        {
            SpawnPoint = spawnPoint;
            _health = _maxHealth;
            IsDead = false;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;
            
            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
                Die(damage);
            }
        }

        protected virtual void Die(int lastDamage)
        {
            IsDead = true;
        }
    }
}