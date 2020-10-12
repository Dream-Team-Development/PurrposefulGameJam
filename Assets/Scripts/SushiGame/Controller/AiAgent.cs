using System.Collections;
using UnityEngine;

namespace SushiGame.Controller
{
    public class AiAgent : MonoBehaviour
    {
        [Header("Ai Behaviour")]
        [SerializeField] private float _speed;
        [SerializeField] private AiController _aiController;
        
        private Color[] _colors = {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow};
        
        public int _startPos;
        public int _nextPos;

        public AiController ControllerAi
        {
            get => _aiController;
            set => _aiController = value;
        }

        private void Awake()
        {
            _aiController = FindObjectOfType<AiController>();
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

        public IEnumerator DeathTime()
        {
            var time = 10f;
            var currentTime = 0f;

            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }

        public IEnumerator ColorChange()
        {
            var alwaysTrue = true;
            var render = GetComponent<SpriteRenderer>();
            var current = Random.Range(0, _colors.Length);
            var next = current + 1;
            var t = 0f;

            if (current == _colors.Length - 1) next = 0;

            while (alwaysTrue)
            {
                if (render.color == _colors[next])
                {
                    t = 0;
                    current++;
                    if (current == _colors.Length - 1) next = 0;
                    else if (current == _colors.Length) current = 0;
                    else next = current + 1;
                }

                t += Time.deltaTime;
                render.color = Color.Lerp(_colors[current], _colors[next], t);
                yield return null;
            }
        }
    }
}
