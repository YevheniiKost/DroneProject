using UnityEngine;
using UnityEngine.Pool;

namespace Enemy.Weapon
{
    public class AutomaticWeapon : WeaponBase
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletLifeTime;
        [SerializeField] private MuzzleFlashMesh _muzzleFlash;
        
        private ObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(() =>
            {
                var bullet = Instantiate(_bulletPrefab);
                bullet.gameObject.SetActive(false);
                return bullet;
            }, OnGetBullet, 
                bullet => bullet.gameObject.SetActive(false));
            
        }

        private void OnGetBullet(Bullet bullet)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.gameObject.SetActive(true);
            bullet.Init(new Bullet.BulletData
            {
                Speed = _bulletSpeed,
                Lifetime = _bulletLifeTime,
                ReturnToPool = _bulletPool.Release
            });
        }

        protected override void Fire()
        {
            base.Fire();
            
            var bullet = _bulletPool.Get();
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            _bulletPool.Get();
            _muzzleFlash.Activate();
        }
    }
}