using SushiGame.Controller;
using UnityEngine;

namespace SushiGame.Spawner
{
    public class AiSpawner : MonoBehaviour
    {
      [Header("Spawn Timer")]
        [SerializeField] private float __spawnRate;
        [SerializeField] private float _timerReduction;
        [SerializeField] private float _elapsedTimeToReduce;
        [Header("Dependencies")]
        [SerializeField] private GameObject[] _aiPrefab;
        [SerializeField] private int[] _spawnWeight;
        [SerializeField] private AiController _aiController;
        private float _spawnTimer;
        private float _timeElapsed;
        private float _previousTime = 0f;
        private bool _gameOver;

        private void OnValidate()
        {
            if (_aiPrefab.Length != _spawnWeight.Length) _spawnWeight = new int[_aiPrefab.Length];
        }

        private void Update()
        {
            if (_gameOver) return;
            _spawnTimer += Time.deltaTime;
            if (!(_spawnTimer > __spawnRate)) return;
            SpawnAi();
            _timeElapsed += _spawnTimer;
            if (_timeElapsed - _previousTime > _elapsedTimeToReduce)
            {
                _previousTime = _timeElapsed;
                if (__spawnRate - _timerReduction > 0) __spawnRate -= _timerReduction;
            }

            _spawnTimer = 0f;
        }

        private void SpawnAi()
        {
            var spawnChance = Random.Range(0, 100);
            GameObject aiToSpawn = null;
            for (int i = 0; i < _aiPrefab.Length; i++)
            {
                if (spawnChance < _spawnWeight[i]) aiToSpawn = _aiPrefab[i];
            }
            if (aiToSpawn == null) return;
            var go = Instantiate(aiToSpawn);
            var goComp = go.GetComponent<AiAgent>();
            StartCoroutine(goComp.DeathTime());
            //StartCoroutine(goComp.ColorChange());
        }
    }
}
