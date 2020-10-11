using UnityEngine;

namespace SushiGame.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _maxDistance = 5f;
        [SerializeField] private Transform _centre;
        [SerializeField] private float _startWeight;
        [SerializeField] private float _startEnergy;
        [SerializeField] private float _sicknessLevel;
        private float _xMov;
        private float _yMov;

        public float Weight
        {
            get => _startWeight;
            set => _startWeight = value;
        }

        public float Energy
        {
            get => _startEnergy;
            set => _startEnergy = value;
        }

        public float SicknessLevel
        {
            get => _sicknessLevel;
            set => _sicknessLevel = value;
        }

        private void Update()
        {
            _xMov = Input.GetAxis("Horizontal");
            _yMov = Input.GetAxis("Vertical");
            var nextPos = transform.position + new Vector3(_xMov, _yMov, 0).normalized * (_speed * Time.deltaTime);

            if (Vector3.Distance(_centre.position, nextPos) > _maxDistance) return;
        
            transform.position += new Vector3(_xMov, _yMov, 0).normalized * (_speed * Time.deltaTime);
        }

        [Header("Debug Circle")]
        [SerializeField] private int _segments = 32;
        [SerializeField] private Color _color = Color.blue;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawEllipse(_centre.position, _centre.forward, _centre.up, _maxDistance, _maxDistance, _segments, _color);
        }
 
        private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0)
        {
            float angle = 0f;
            Quaternion rot = Quaternion.LookRotation(forward, up);
            Vector3 lastPoint = Vector3.zero;
            Vector3 thisPoint = Vector3.zero;
 
            for (int i = 0; i < segments + 1; i++)
            {
                thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
                thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;
 
                if (i > 0)
                {
                    Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
                }
 
                lastPoint = thisPoint;
                angle += 360f / segments;
            }
        }
#endif
    }
}
