using Cinemachine;
using Enemy;
using UnityEngine;

namespace YT.Game.Projectiele
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _damage = 100;
        [SerializeField] private float _explosionForce = 100f;
        [SerializeField] private float _explosionRadius = 10f;
        [SerializeField] private LayerMask _explosionMask;
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private ParticleSystem _explosionEffectPrefab;
    
        private bool _isDroped;
    
        public void SetAsChild(Transform parent)
        {
            Transform myTransform;
            (myTransform = transform).SetParent(parent);
            myTransform.localPosition = Vector3.zero;
            myTransform.localRotation = Quaternion.identity;
            _rigidbody.isKinematic = true;
        }
        
        public void Drop(float force)
        {
            transform.SetParent(null);
            transform.localRotation = Quaternion.identity;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Vector3.down * force, ForceMode.Impulse);
            _isDroped = true;
        }
    
        private void OnCollisionEnter(Collision other)
        {
            if(!_isDroped)
                return;
            
            var results = new Collider[30];
            var size = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, results, _explosionMask);
            for (var i = 0; i < size; i++)
            {
                var enemy = results[i].GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                    if(enemy is IExplodable explodable)
                        explodable.Explode(transform.position, _explosionForce, _explosionRadius);
                }
            }
            
            GenerateImpulse();
            PlayVisualEffect();
            Destroy(gameObject);
        }

        private void GenerateImpulse()
        {
            _impulseSource.GenerateImpulse();
        }

        private void PlayVisualEffect()
        {
            var effect = Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
            effect.Play();
        }
    }
}
