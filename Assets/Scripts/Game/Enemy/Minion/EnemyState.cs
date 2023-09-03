namespace Enemy
{
    public abstract class EnemyState
    {
        public abstract StateType StateType { get; }
        
        protected readonly EnemyMinion EnemyMinion;
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyState(EnemyMinion enemyMinion, EnemyStateMachine stateMachine)
        {
            EnemyMinion = enemyMinion;
            StateMachine = stateMachine;
        }

        public abstract void Update(float delta);
        public abstract void Enter();
        public abstract void Exit();
    }
}