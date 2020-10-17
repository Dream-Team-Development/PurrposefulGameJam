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
        //[SerializeField, Tooltip("Grams, convert to KG for display")] private float _weight;
        //[SerializeField] private float _energy;
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
        [SerializeField] private Slider _energyDisplay;
        [SerializeField] private Text _sicknessDisplay;
        [SerializeField] private float _circleSpeed;
        [SerializeField] private float _circleSize;

        private bool _isSick;

        private float _weight, _energy;
        private float _xMov;
        private float _yMov;

        public CatObject CatType { get; set; }

        //Properties for weight, energy and sickness effected by picked up items
        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                UpdateWeight();
            }
        }

        public float Energy
        {
            get => _energy;
            set
            { 
                _energy = value;
                UpdateEnergy();
            }
        }

        public float SicknessLevel
        {
            get => _sicknessLevel;
            set => _sicknessLevel = value;
        }

        private void Start()
        {
            _animator.runtimeAnimatorController = CatType.sushiGameAnimator;
            
            UpdateWeight();
            UpdateEnergy();
        }

        private void FixedUpdate()
        {
            if (!SushiGameManager.Instance.Playing) return;
            
            //Handles preventing start weight dropping below zero, reducing weight over time, and displaying information
            if (_weight < 0) _weight = 0;
            if (_weight > 0) _weight -= _weightLossRate * Time.deltaTime;
            UpdateWeight();
            //if (_weightDisplay) _weightDisplay.text = $"{(_weight / 100):#.0}kg"; // Convert grams to KG
            //if (_energyDisplay) _energyDisplay.text = "Energy: " + _energy;
            if (_sicknessDisplay) _sicknessDisplay.text = "Sickness: " + _sicknessLevel;

            //If the cat gets sick this will block movement and create new movement
            if (_sicknessLevel > 0)
            {
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
            
            UpdateAnimator();
            
            //Set velocity to zero if no input (stops sliding)
            if (_yMov == 0 && _xMov == 0) _rb.velocity = Vector2.zero;

            _rb.velocity += new Vector2(_xMov, _yMov).normalized * _speed;
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);

            //Interact, this will run for every object it collides with so collider size needs to only fit one item in at a time
            if (!Input.GetKey(_interactKey)) return;
            var hits = Physics2D.OverlapCircle(transform.position, _radius, -8);
            if (!hits) return;
            var interact = hits.GetComponent<IInteract>();
            interact?.Interact(this);
        }


        private void UpdateWeight()
        {
            //if (_weightDisplay) _weightDisplay.text = $"{(_weight / 100):#.0}kg";
            if (_weightDisplay) _weightDisplay.text = (Mathf.Round(_weight * 10f) / 10f) + "kg";
        }


        private void UpdateEnergy()
        {
            if (!_energyDisplay) return;
            
            _energyDisplay.value = _energy / 100;
        }


        private void UpdateAnimator()
        {
            if (_yMov > 0) _animator.SetInteger(_animatorParameter, 1);
            else if(_xMov > 0) _animator.SetInteger(_animatorParameter, 2);
            else if (_yMov < 0) _animator.SetInteger(_animatorParameter, 3);
            else if (_xMov < 0) _animator.SetInteger(_animatorParameter, 4);
            else _animator.SetInteger(_animatorParameter, 0);
        }
        
        
        //Used to draw interaction area
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
