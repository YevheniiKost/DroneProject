using UnityEngine;

namespace Enemy.Minion
{
    public class EnemyMinionAnimation : MonoBehaviour
    {
        private const string WalkParameter = "IsWalk";
        private const string DeathParameter = "Death";
        
        
        [SerializeField] private Animator _animator;
        
        private int _walkParameterHash = Animator.StringToHash(WalkParameter);
        private int _deathParameterHash = Animator.StringToHash(DeathParameter);
        
        public void SetWalkAnimation(bool value)
        {
            _animator.SetBool(_walkParameterHash, value);
        }
        
        public void SetDeathAnimation()
        {
            _animator.SetTrigger(_deathParameterHash);
        }

        public void SetAttackAnimation()
        {
            
        }
    }
}