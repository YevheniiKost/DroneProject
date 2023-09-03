using System;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyBase[] _enemyPrefabs;
        [SerializeField] private int _minEnemyCount;
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private float _maxDistanceFromCenter;
        
        private void Start()
        {
            int enemyCount = UnityEngine.Random.Range(_minEnemyCount, _maxEnemyCount);
            for (int i = 0; i < enemyCount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
                Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * _maxDistanceFromCenter;
                Quaternion randomRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, UnityEngine.Random.Range(0, 360), transform.rotation.eulerAngles.z);
                randomPosition.y = transform.position.y;
                var enemy = Instantiate(_enemyPrefabs[randomIndex], transform.position + randomPosition, randomRotation);
                enemy.Init(transform);
                enemy.name = _enemyPrefabs[randomIndex].name;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }
    }
}