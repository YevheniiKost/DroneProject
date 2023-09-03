using UnityEngine;

namespace Enemy
{
    public class DetectState : EnemyState
    {
        public override StateType StateType => StateType.Detect;

        private readonly float _detectionTime = 3f;
        private float _currentTime;
        private readonly EnemyWeaponized _weaponized;
       
        public DetectState(EnemyMinion enemyMinion, EnemyStateMachine stateMachine) : base(enemyMinion, stateMachine)
        {
            _weaponized = enemyMinion as EnemyWeaponized;
        }

        public override void Update(float delta)
        {
            if (!_weaponized.IsTargetInRange())
            {
                StateMachine.ReturnToPreviousState();
                return;
            }

            if (_currentTime > 0)
            {
                _currentTime -= delta;
                EnemyMinion.transform.rotation =
                    Quaternion.LookRotation(_weaponized.Target.transform.position - EnemyMinion.transform.position);
            }
            else
                StateMachine.SetState(StateType.Attack);
        }

        public override void Enter()
        {
            _currentTime = _detectionTime;
        }

        public override void Exit()
        {
            
        }
    }
}