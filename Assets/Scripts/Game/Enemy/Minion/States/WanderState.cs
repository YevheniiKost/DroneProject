using UnityEngine;

namespace Enemy
{
    public class WanderState : EnemyState
    {
        public override StateType StateType => StateType.Wander;

        private readonly float _minWanderTime = 1f;
        private readonly float _maxWanderTime = 3f;
        private readonly float _wanderSpeed = 2f;
        private readonly float _maxDistanceFromCenter = 5f;
        
        private float _currentWalkCycleTime;

        public WanderState(EnemyMinion enemyMinion, EnemyStateMachine stateMachine) : base(enemyMinion, stateMachine)
        {
        }

        public override void Update(float delta)
        {
            if (_currentWalkCycleTime <= 0)
            { 
                CreateNewWalkCycle();
            }
            else
            {
                EnemyMinion.MoveForward(_wanderSpeed * delta);
                _currentWalkCycleTime -= delta;
            }

            if (Vector3.SqrMagnitude(EnemyMinion.transform.position - EnemyMinion.SpawnPoint.position) >
                _maxDistanceFromCenter * _maxDistanceFromCenter)
            {
                EnemyMinion.transform.rotation = Quaternion.LookRotation(EnemyMinion.SpawnPoint.position - EnemyMinion.transform.position);
            }
        }

        private void CreateNewWalkCycle()
        {
            _currentWalkCycleTime = UnityEngine.Random.Range(_minWanderTime, _maxWanderTime);
            var randomDirection = UnityEngine.Random.insideUnitCircle;
            var randomDirection3D = new Vector3(randomDirection.x, EnemyMinion.transform.position.y, randomDirection.y);
            var randomRotation = Quaternion.LookRotation(randomDirection3D);
            EnemyMinion.transform.rotation = randomRotation;
        }

        public override void Enter()
        {
            EnemyMinion.Animation.SetWalkAnimation(true);
        }

        public override void Exit()
        {
            EnemyMinion.Animation.SetWalkAnimation(false);
        }
    }
}