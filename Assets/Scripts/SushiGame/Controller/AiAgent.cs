using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SushiGame.Controller
{
    public class AiAgent : MonoBehaviour
    {
        [Header("Ai Behaviour")]
        [SerializeField] private float _speed;
        [SerializeField] private AiController _aiController;
        [SerializeField] private float _radius;
        //Colours used for rave party time
        private Color[] _colors = {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow};
        
        public int _startPos;
        public int _nextPos;
        
        //Draw area used to check if other items, debug to show visually
        //Code further down checks if the spawn position has another item nearby
        //Prevents items spawning on top of each other
        //This debugs that for you
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void Awake()
        {
            //Grab the AI controller for movement
            _aiController = FindObjectOfType<AiController>();
            //Get a position out of the node list
            _startPos = Random.Range(0, _aiController.Positions.Length);
            //Check position isn't occupado
            while (Physics2D.OverlapCircle(_aiController.Positions[_startPos], _radius, -8))
            {
                _startPos = Random.Range(0, _aiController.Positions.Length);
            }

            transform.position = _aiController.Positions[_startPos];
        }

        private void Update()
        {
            //Loop around the array of positions
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
            //Kill the item
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
            //Rave time
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
