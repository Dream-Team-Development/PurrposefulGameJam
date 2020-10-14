using UnityEngine;

namespace RhythmGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MusicNote _notePrefab;
        [SerializeField] protected GameObject _hitPoint;
        [SerializeField] protected string[] _hitFeedback = {"Nice!"};

        protected MusicNote _noteToHit;

        private void Start()
        {
            _notePrefab.HitPos = _hitPoint.transform.position;
        }


        public void ProduceNote()
        {
            _noteToHit = Instantiate(_notePrefab, _notePrefab.SpawnPos, Quaternion.identity);
            var position = _hitPoint.transform.position;
            
            _noteToHit.HitPos = new Vector2(position.x, position.y);
        }
    }
}
