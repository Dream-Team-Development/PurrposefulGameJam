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
        [SerializeField, Tooltip("Order matches spawn weight below")] private GameObject[] _aiPrefab;
        [SerializeField, Tooltip("Descending chance from 100 to 0")] private int[] _spawnWeight;
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
            //This was taken from my ludum dare game, spawns an object every so many times (spawn rate)
            if (_gameOver) return;
            _spawnTimer += Time.deltaTime;
            if (!(_spawnTimer > __spawnRate)) return;
            SpawnAi();
            _timeElapsed += _spawnTimer;
            //Reduce timer if so long has passed, can be used to speed up item spawning if desired.
            if (_timeElapsed - _previousTime > _elapsedTimeToReduce)
            {
                _previousTime = _timeElapsed;
                if (__spawnRate - _timerReduction > 0) __spawnRate -= _timerReduction;
            }

            _spawnTimer = 0f;
        }

        private void SpawnAi()
        {
            //Spawn AI by % chance
            var spawnChance = Random.Range(0, 100);
            GameObject aiToSpawn = null;
            for (int i = 0; i < _aiPrefab.Length; i++)
            {
                if (spawnChance < _spawnWeight[i]) aiToSpawn = _aiPrefab[i];
            }
            if (aiToSpawn == null) return;
            var go = Instantiate(aiToSpawn);
            var goComp = go.GetComponent<AiAgent>();
            //Coroutine used to destroy the object after so long, found on the AI agent itself (items)
            StartCoroutine(goComp.DeathTime());
            //Code from my fun time creating trippy shit, left it here for fun. Uncomment if you want it to rave
            //Beware, will throw errors when item is destroyed
            //StartCoroutine(goComp.ColorChange());
        }
    }
}
