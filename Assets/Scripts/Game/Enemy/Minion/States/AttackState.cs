using UnityEngine;

namespace Enemy
{
    public class AttackState : EnemyState
    {
        public override StateType StateType => StateType.Attack;
        
        private readonly EnemyWeaponized _weaponized;
        
        public AttackState(EnemyMinion enemyMinion, EnemyStateMachine stateMachine) : base(enemyMinion, stateMachine)
        {
            _weaponized = enemyMinion as EnemyWeaponized;
        }

        public override void Update(float delta)
        {
            if (!_weaponized.IsTargetInRange())
            {
                StateMachine.SetState(StateType.Wander);
                return;
            }

            if (_weaponized.IsTargetInAttackRange())
            {
                EnemyMinion.Animation.SetAttackAnimation();
                _weaponized.Attack();
                var targetPosition = _weaponized.Target.transform.position;
                EnemyMinion.transform.rotation =
                    Quaternion.LookRotation(targetPosition - EnemyMinion.transform.position);
                var weaponTransform = _weaponized.Weapon.transform;
                weaponTransform.rotation = Quaternion.LookRotation(targetPosition - weaponTransform.position);
            }
            else
            {
                StateMachine.SetState(StateType.Detect);
            }
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

       
    }
}