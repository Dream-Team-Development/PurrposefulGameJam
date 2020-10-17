using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmGame
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MusicNote _notePrefab;
        [SerializeField] private Slider _energyBarSlider;
        [SerializeField] private TMP_Text _weightText;
        [SerializeField] protected float _energyChange, _weightChange;
        [SerializeField] protected float _weightLossRate;
        [SerializeField] protected HitPoint _hitPoint;
        [SerializeField] protected string[] _hitFeedback = {"Nice!"};

        [Header("Animations")]
        [SerializeField] protected Animator _catAnimator;
        [SerializeField] private string _animatorParameterName;
        [SerializeField] private int _totalDanceAnimations;
        [SerializeField] private int _animationSwitchRate;

        protected float _weight, _energy;
        protected MusicNote _noteToHit;
        protected int _danceIndex;
        
        public CatObject CatType { get; set; }
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


        private void Start()
        {
            _notePrefab.HitPos = _hitPoint.gameObject.transform.position;

            if (!_catAnimator) _catAnimator = GetComponent<Animator>();
            _catAnimator.runtimeAnimatorController = CatType.rhythmGameAnimator;

            _danceIndex = 0;
            _totalDanceAnimations *= _animationSwitchRate;
        }


        public void ProduceNote()
        {
            _noteToHit = Instantiate(_notePrefab, _notePrefab.SpawnPos, Quaternion.identity);
            var position = _hitPoint.gameObject.transform.position;
            
            _noteToHit.HitPos = new Vector2(position.x, position.y);
            _hitPoint.SpriteRenderer.sprite = _noteToHit.SpriteRenderer.sprite;
        }


        protected void UpdateWeight()
        {
            if (_weightText) _weightText.text = (Mathf.Round(_weight * 10f) / 10f) + "kg";
        }


        protected void UpdateEnergy()
        {
            _energyBarSlider.value = _energy / 100;
        }


        protected void UpdateStreak(bool hit)
        {
            if (!hit) return;
            
            _danceIndex++;
            if (_danceIndex > _totalDanceAnimations) _danceIndex = 0;
            
            _catAnimator.SetInteger(_animatorParameterName, _danceIndex);
        }
    }
}
