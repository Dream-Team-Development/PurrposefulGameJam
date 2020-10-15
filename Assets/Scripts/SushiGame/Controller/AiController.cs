using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SushiGame.Controller
{
    public class AiController : MonoBehaviour
    {
        [Header("Movement Path")]
        [SerializeField] private int _segments = 32;
        [SerializeField] private float _xRadius = 2f;
        [SerializeField] private float _yRadius = 2f;
        [SerializeField] private Transform _centre;
        [SerializeField] private Color _colour = Color.blue;
        
        private Vector3[] _positions = new Vector3[0];
        private float _lastXRadius;
        private float _lastYRadius;

        public Vector3[] Positions => _positions;
        //This is run entirely in editor for adjusting the node pathway for items to travel
        //You shouldn't need to edit this script
        #region Create Positions

        private void OnValidate()
        {
            if (_positions.Length != _segments || _xRadius != _lastXRadius || _yRadius != _lastYRadius)
            {
                _positions = new Vector3[_segments];
                _lastXRadius = _xRadius;
                _lastYRadius = _yRadius;

                var angle = 0f;
                var rot = Quaternion.LookRotation(_centre.forward, _centre.up);
                var thisPoint = Vector3.zero;
 
                for (var i = 0; i < _segments; i++)
                {
                    thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * _xRadius;
                    thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * _yRadius;

                    _positions[i] = rot * thisPoint + _centre.position;

                    angle += 360f / _segments;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            DrawEllipse(_colour);
        }
 
        private void DrawEllipse(Color color, float duration = 0)
        {
 
            for (int i = 0; i < _positions.Length + 1; i++)
            {
                if (i > 0 && i != _positions.Length) Debug.DrawLine(_positions[i - 1], _positions[i], color, duration);
                
                else if (i == _positions.Length) Debug.DrawLine(_positions[i -1], _positions[0], color, duration);
            }
        }
        #endregion
    }
}
