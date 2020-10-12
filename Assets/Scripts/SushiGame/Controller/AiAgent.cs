using UnityEngine;

namespace SushiGame.Controller
{
    public class AiAgent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private AiController _aiController;
        
        public int _startPos;
        public int _nextPos;
        private void Awake()
        {
            _startPos = Random.Range(0, _aiController.Positions.Length);
            transform.position = _aiController.Positions[_startPos];
        }

        private void Update()
        {
            if (_startPos == _aiController.Positions.Length - 1) _nextPos = 0;
            else _nextPos = _startPos + 1;
            var direction = _aiController.Positions[_nextPos] - transform.position;

            transform.position += direction.normalized * (_speed * Time.deltaTime);

            if (!(Vector3.Distance(transform.position, _aiController.Positions[_nextPos]) < 0.2f)) return;
            if (_startPos == _aiController.Positions.Length - 1) _startPos = 0;
            else _startPos++;
        }
    }
}
