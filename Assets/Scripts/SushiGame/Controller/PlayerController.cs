using SushiGame.FoodStuff;
using UnityEngine;

namespace SushiGame.Controller
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _maxDistance = 5f;
        [SerializeField] private Transform _centre;
        [SerializeField] private float _startWeight;
        [SerializeField] private float _startEnergy;
        [SerializeField] private float _sicknessLevel;
        [SerializeField] private Rigidbody2D _rb;
        
        [Header("Interact")]
        [SerializeField] private KeyCode _interactKey;
        [SerializeField] private float _radius;
        
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
            if (Input.GetKeyDown(_interactKey))
            {
                var hits = Physics2D.OverlapCircle(transform.position, _radius, -8);
                var interact = hits.GetComponent<IInteract>();
                interact?.Interact(this);
            }
            
            _xMov = Input.GetAxis("Horizontal");
            _yMov = Input.GetAxis("Vertical");

            _rb.velocity += new Vector2(_xMov, _yMov).normalized * _speed;
        }
    }
}
