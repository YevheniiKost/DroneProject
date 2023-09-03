namespace Enemy
{
    public class IdleState : EnemyState
    {
        public override StateType StateType => StateType.Idle;
        
        public IdleState(EnemyMinion enemyMinion, EnemyStateMachine stateMachine) : base(enemyMinion, stateMachine)
        {
        }

        public override void Update(float delta)
        {
        }

        public override void Enter()
        {
            EnemyMinion.Animation.SetWalkAnimation(false);
        }

        public override void Exit()
        { }

       
    }
}