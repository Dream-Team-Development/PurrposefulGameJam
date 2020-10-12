using UnityEngine;

namespace RhythmGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MusicNote _notePrefab;
        [SerializeField] protected GameObject _hitPoint;
        [SerializeField] protected string[] _hitFeedback = {"Nice!"};

        protected MusicNote _noteToHit;

        
        public void ProduceNote()
        {
            _noteToHit = Instantiate(_notePrefab, _notePrefab.SpawnPos, Quaternion.identity);
            _noteToHit.HitPos = new Vector2(_hitPoint.transform.position.x, _hitPoint.transform.position.y);
        }
    }
}
