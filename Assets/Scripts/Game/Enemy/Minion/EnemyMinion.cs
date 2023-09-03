using Enemy.Minion;
using UnityEngine;

namespace Enemy
{
    public class EnemyMinion : EnemyBase, IExplodable
    {
        public EnemyMinionAnimation Animation => _animation;
        public Rigidbody Rigidbody => _rigidbody;
        
        [SerializeField] private Renderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Material _livingMaterial;
        [SerializeField] private Material _deadMaterial;
        [SerializeField] private EnemyMinionAnimation _animation;

        protected EnemyStateMachine StateMachine;

        public override void Init(Transform spawnPoint)
        {
            base.Init(spawnPoint);
            _meshRenderer.material = _livingMaterial;
            PrepareStates();
        }

        protected override void Die(int laseDamage)
        {
            base.Die(laseDamage);
            _meshRenderer.material = _deadMaterial;
            StateMachine.SetState(StateType.Dead);
        }

        public void Explode(Vector3 position, float force, float radius)
        {
            var direction = (transform.position - position).normalized;
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
        
        public void MoveForward(float speed)
        {
            transform.position += transform.forward * speed;
        }

        private void Update()
        {
            if (IsDead)
                return;
            
            StateMachine.Update(Time.deltaTime);
        }
        
        private void PrepareStates()
        {
            StateMachine = new EnemyStateMachine(this);
            StateMachine.SetState(StateType.Wander);
        }
    }


    public enum StateType
    {
        None,
        Wander,
        Detect,
        Attack,
        Idle,
        Dead
    }
}