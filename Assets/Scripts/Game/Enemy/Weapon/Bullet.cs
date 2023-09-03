using System;
using UnityEngine;
using YT.Game.Drone;

namespace Enemy.Weapon
{
    internal class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _lifetime;
        private Action<Bullet> _returnToPool;
        
        private float _currentLifetime;
        
        private void OnEnable()
        {
            _currentLifetime = _lifetime;
        }
        
        public void Init(BulletData data)
        {
            _speed = data.Speed;
            _lifetime = data.Lifetime;
            _returnToPool = data.ReturnToPool;
        }
        
        private void Update()
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
            
            _currentLifetime -= Time.deltaTime;
            if (_currentLifetime <= 0)
            {
                _returnToPool?.Invoke(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Drone player))
            {
                player.Damage();
                _returnToPool?.Invoke(this);
            }
        }

        public class BulletData
        {
            public float Speed;
            public float Lifetime;
            public Action<Bullet> ReturnToPool;
        }
    }
}