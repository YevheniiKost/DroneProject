using UnityEngine;

namespace Enemy.Weapon
{
    public class MuzzleFlashMesh : MonoBehaviour
    {
        [SerializeField] private float _flashTime = 0.1f;
        
        
        public void Activate()
        {
            gameObject.SetActive(true);
            Invoke(nameof(Deactivate), _flashTime);
        }
        
        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}