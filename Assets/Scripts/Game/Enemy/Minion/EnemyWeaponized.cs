using Enemy.Weapon;
using UnityEngine;
using YT.Game.Drone;

namespace Enemy
{
    public class EnemyWeaponized : EnemyMinion 
    {
        public Drone Target => _target;
        public WeaponBase Weapon => _weapon;
        
        [SerializeField] private EnemyDroneDetector _detector;
        [SerializeField] private float _detectRange = 10f;
        [SerializeField] private float _attackRange = 5f;
        [SerializeField] private WeaponBase _weapon;

        private Drone _target;
        
        public override void Init(Transform spawnPoint)
        {
            base.Init(spawnPoint);
            _detector.OnDroneDetected += OnDroneDetected;
        }
        
        public bool IsTargetInRange()
        {
            if (_target == null)
                return false;
            
            var distance = Vector3.Distance(transform.position, _target.transform.position);
            return distance <= _detectRange;
        }
        
        public bool IsTargetInAttackRange()
        {
            if (_target == null)
                return false;
            
            var distance = Vector3.Distance(transform.position, _target.transform.position);
            return distance <= _attackRange;
        }

        private void OnDroneDetected(Drone obj)
        {
            _target = obj;
            StateMachine.SetState(StateType.Detect);
        }


        public void Attack()
        {
            _weapon.Attack();
        }
    }
}