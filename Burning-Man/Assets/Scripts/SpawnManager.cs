using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan.Manager
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [Header("Level Manage")]
        [SerializeField] private Transform _levelStart , _levelEnd, _player;

        [SerializeField] private GameObject[] _enemies;
        [SerializeField] private int _enemyNumber;

        [SerializeField] private GameObject[] _obstacles;
        [SerializeField] private int _obstacleNumber;

        [SerializeField] private GameObject _spawnPointPrefab;
        private float _gap;
        public List<GameObject> _spawnPoints = new List<GameObject>();

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _player = _levelStart;

            LevelStart();
        }

        public void LevelStart()
        {
            var TotalNumber = 2 *( _obstacleNumber + _enemyNumber);
            _gap = Mathf.Abs(_levelEnd.position.y - _levelStart.position.y)/TotalNumber;

            for (float i = 0; i < TotalNumber; i++)
            {
                GameObject created = Instantiate(_spawnPointPrefab, new Vector2(0, (-i*_gap) - 10 ), Quaternion.identity);
                _spawnPoints.Add(created);
            }
            CreateAEnemy();
            CreateAObstacle();
        }
        public void CreateAEnemy()
        {
            for (int j = 0; j < _obstacleNumber; j++)
            {
                int _randomIndexPoint = Random.Range(0, _spawnPoints.Count);
                int _randomIndexEnemies = Random.Range(0, _enemies.Length);
                Instantiate(_enemies[_randomIndexEnemies], _spawnPoints[_randomIndexPoint].transform.position, Quaternion.identity);
                _spawnPoints.Remove(_spawnPoints[_randomIndexPoint]);
            }
        }
        public void CreateAObstacle()
        {
            for (int j = 0; j < _enemyNumber; j++)
            {
                int _randomIndexPointer = Random.Range(0, _spawnPoints.Count);
                int _randomIndexObstacle = Random.Range(0, _enemies.Length);
                Instantiate(_enemies[_randomIndexObstacle], _spawnPoints[_randomIndexPointer].transform.position, Quaternion.identity);
                _spawnPoints.Remove(_spawnPoints[_randomIndexPointer]);

            }
        }
    }
}
