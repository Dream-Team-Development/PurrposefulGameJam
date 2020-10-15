using System;
using SushiGame.FoodStuff;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SushiGame.Controller
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private float _speed = 2f;
        [SerializeField, Tooltip("Grams, convert to KG for display")] private float _startWeight;
        [SerializeField] private float _startEnergy;
        [SerializeField] private float _sicknessLevel;
        [SerializeField] private Rigidbody2D _rb;

        [Header("Player Input")]
        [SerializeField] private KeyCode _upKey;
        [SerializeField] private KeyCode _leftKey;
        [SerializeField] private KeyCode _downKey;
        [SerializeField] private KeyCode _rightKey;
        
        [Header("Animation")]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animatorParameter;
        
        [Header("Interact")]
        [SerializeField] private KeyCode _interactKey;
        [SerializeField] private float _radius;
        
        [Header("Cat Info")]
        [SerializeField] private TMP_Text _weightDisplay;
        [SerializeField] private float _weightLossRate;
        [SerializeField] private Text _energyDisplay;
        [SerializeField] private Text _sicknessDisplay;
        [SerializeField] private float _circleSpeed;
        [SerializeField] private float _circleSize;

        private bool _isSick;

        private float _xMov;
        private float _yMov;

        //Properties for weight, energy and sickness effected by picked up items
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
            
            //Handles preventing start weight dropping below zero, reducing weight over time, and displaying information
            if (_startWeight < 0) _startWeight = 0;
            if (_startWeight > 0) _startWeight -= _weightLossRate * Time.deltaTime;
            if (_weightDisplay) _weightDisplay.text = $"{(_startWeight / 100):#.0}kg"; // Convert grams to KG
            if (_energyDisplay) _energyDisplay.text = "Energy: " + _startEnergy;
            if (_sicknessDisplay) _sicknessDisplay.text = "Sickness: " + _sicknessLevel;

            //If the cat gets sick this will block movement and create new movement
            if (_sicknessLevel > 0)
            {
                _animator.SetInteger(_animatorParameter, 5);
                
                _sicknessLevel -= Time.deltaTime;
                
                var direction = Vector3.zero - transform.position;
                
                //Create position for x and y using sine and cosine, using circle speed and size to adjust size.
                //Adjusting circle speed and size will change the behaviour, you can get a faster spin with a large size and smaller circle
                //Or side to side walking with a larger circle and relatively slower speed
                var xPos = Mathf.Sin(Time.time * _circleSpeed) * _circleSize;
                var yPos = Mathf.Cos(Time.time * _circleSpeed) * _circleSize;

                direction.x += xPos;
                direction.y += yPos;

                _rb.velocity += new Vector2(direction.x, direction.y).normalized * _speed;
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);
                return;
            }
            
            //Prevent sickness level going below zero
            if (_sicknessLevel < 0)
            {
                _sicknessLevel = 0;
                _animator.SetInteger(_animatorParameter, 0);
            }
            
            //Get vertical input
            if (Input.GetKey(_upKey)) _yMov = 1;
            else if (Input.GetKey(_downKey)) _yMov = -1;
            else _yMov = 0;
            
            //Get horizontal input
            if (Input.GetKey(_leftKey)) _xMov = -1;
            else if (Input.GetKey(_rightKey)) _xMov = 1;
            else _xMov = 0;
            
            //Set velocity to zero if no input (stops sliding)
            if (_yMov == 0 && _xMov == 0) _rb.velocity = Vector2.zero;
            
            if (_yMov > 0) _animator.SetInteger(_animatorParameter, 1);
            else if(_xMov > 0) _animator.SetInteger(_animatorParameter, 2);
            else if (_yMov < 0) _animator.SetInteger(_animatorParameter, 3);
            else if (_xMov < 0) _animator.SetInteger(_animatorParameter, 4);
            else _animator.SetInteger(_animatorParameter, 0);

            _rb.velocity += new Vector2(_xMov, _yMov).normalized * _speed;
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);

            //Interact, this will run for every object it collides with so collider size needs to only fit one item in at a time
            if (!Input.GetKey(_interactKey)) return;
            var hits = Physics2D.OverlapCircle(transform.position, _radius, -8);
            if (!hits) return;
            var interact = hits.GetComponent<IInteract>();
            interact?.Interact(this);
        }
        
        //Used to draw interaction area
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
