namespace Enemy
{
    public class DeathState : EnemyState
    {
        public override StateType StateType => StateType.Dead;
        
        public DeathState(EnemyMinion enemyMinion, EnemyStateMachine stateMachine) : base(enemyMinion, stateMachine)
        {
        }
        
        public override void Update(float delta)
        {
            
        }

        public override void Enter()
        {
            EnemyMinion.Animation.SetDeathAnimation();
            EnemyMinion.Rigidbody.isKinematic = false;
            EnemyMinion.Rigidbody.useGravity = true;
        }

        public override void Exit()
        { }
    }
}