using System;
using SushiGame.FoodStuff;
using UnityEngine;
using UnityEngine.UI;

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
        
        [Header("Cat Info")]
        [SerializeField] private Text _weightDisplay;
        [SerializeField] private float _weightLossRate;
        [SerializeField] private Text _energyDisplay;
        [SerializeField] private Text _sicknessDisplay;
        [SerializeField] private float _sicknessLossWeight;
        [SerializeField] private float _circleSpeed;
        [SerializeField] private float _circleSize;

        private bool _isSick;

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

        private void FixedUpdate()
        {
            if (_startWeight > 0) _startWeight -= _weightLossRate * Time.deltaTime;
            if (_startWeight < 0) _startWeight = 0;
            
            if (_weightDisplay) _weightDisplay.text = "Weight: " + _startWeight;
            if (_energyDisplay) _energyDisplay.text = "Energy: " + _startEnergy;
            if (_sicknessDisplay) _sicknessDisplay.text = "Sickness: " + _sicknessLevel;

            if (_sicknessLevel > 0)
            {
                if (!_isSick) _isSick = true;
                _sicknessLevel -= Time.deltaTime;
                
                var direction = Vector3.zero - transform.position;

                var xPos = Mathf.Sin(Time.time * _circleSpeed) * _circleSize;
                var yPos = Mathf.Cos(Time.time * _circleSpeed) * _circleSize;

                direction.x += xPos;
                direction.y += yPos;

                _rb.velocity += new Vector2(direction.x, direction.y).normalized * _speed;
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);
                return;
            }

            if (_sicknessLevel < 0) _sicknessLevel = 0;
            
            if (_isSick) _isSick = false;
            
            _xMov = Input.GetAxis("Horizontal");
            _yMov = Input.GetAxis("Vertical");

            _rb.velocity += new Vector2(_xMov, _yMov).normalized * _speed;
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);
            
            if (Input.GetKey(_interactKey) && !_isSick)
            {
                var hits = Physics2D.OverlapCircle(transform.position, _radius, -8);
                if (!hits) return;
                var interact = hits.GetComponent<IInteract>();
                interact?.Interact(this);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
