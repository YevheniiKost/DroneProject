namespace Enemy
{
    public class EnemyStateMachine
    {
        private EnemyState _currentState;
        private EnemyState _previousState;
        private readonly EnemyMinion _enemy;

        public EnemyStateMachine(EnemyMinion enemy)
        {
            _enemy = enemy;
        }

        public void SetState(StateType type)
        {
            _currentState?.Exit();
            _previousState = _currentState;
            _currentState = GetState(type, _enemy, this);
            _currentState.Enter();
        }
        
        public void Update(float delta)
        {
            _currentState?.Update(delta);
        }

        public void ReturnToPreviousState()
        {
            SetState(_previousState.StateType);
        }
        
        private static EnemyState GetState(StateType type, EnemyMinion enemyMinion, EnemyStateMachine stateMachine)
        {
            switch (type)
            {
                case StateType.Wander:
                    return new WanderState(enemyMinion, stateMachine);
                case StateType.Idle:
                    return new IdleState(enemyMinion, stateMachine);
                case StateType.Dead:
                    return new DeathState(enemyMinion, stateMachine);
                case StateType.Attack:
                    return new AttackState(enemyMinion, stateMachine);
                case  StateType.Detect:
                    return new DetectState(enemyMinion, stateMachine);
                default:
                    return null;
            }
        }
    }
}