using UnityEngine;

namespace RhythmGame
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MusicNote _notePrefab;
        [SerializeField] protected GameObject _hitPoint;
        [SerializeField] protected string[] _hitFeedback = {"Nice!"};
        
        [Header("Animations")]
        [SerializeField] protected Animator _catAnimator;
        [SerializeField] private string _animatorParameterName;
        [SerializeField] private int _totalDanceAnimations;
        [SerializeField] private int _animationSwitchRate;

        protected MusicNote _noteToHit;
        protected int _danceIndex;

        private void Start()
        {
            _notePrefab.HitPos = _hitPoint.transform.position;

            if (!_catAnimator) _catAnimator = GetComponent<Animator>();

            _danceIndex = 0;
            _totalDanceAnimations *= _animationSwitchRate;
        }


        public void ProduceNote()
        {
            _noteToHit = Instantiate(_notePrefab, _notePrefab.SpawnPos, Quaternion.identity);
            var position = _hitPoint.transform.position;
            
            _noteToHit.HitPos = new Vector2(position.x, position.y);
        }


        protected void UpdateStreak(bool hit)
        {
            if (hit) _danceIndex++;
            else _danceIndex = 0;

            if (_danceIndex > _totalDanceAnimations) _danceIndex = 1;
            
            _catAnimator.SetInteger(_animatorParameterName, _danceIndex);
        }
    }
}
