using UnityEngine;

namespace RhythmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MusicNote : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private int _points = 10;
        [SerializeField] private Vector2 _spawnPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _hitPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _despawnPos = new Vector2(0, -5);

        private SongManager _songManager;

        public Vector2 SpawnPos => _spawnPos;


        private void Start()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _songManager = SongManager.Instance;
        }


        private void Update()
        {
            // Stop moving notes if music is not playing
            if(!_songManager.Audio.isPlaying) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (transform.position.y <= _hitPos.y + 1 && transform.position.y >= _hitPos.y - 1)
                {
                    Debug.Log("Hit at " + transform.position.y);
                    PointsManager.Instance.Points += _points;
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Missed at " + transform.position.y);
                    PointsManager.Instance.Points -= _points / 2;
                    Destroy(gameObject);
                }
            }

            // Move note in time to song
            transform.position = Vector2.Lerp(
                _spawnPos,
                _despawnPos,
                (_songManager.BeatsShownInAdvance - (_songManager.Notes[_songManager.NextIndex - 1] - _songManager.SongPosInBeats)) / _songManager.BeatsShownInAdvance
            );

            if (transform.position.y == _despawnPos.y)
            {
                PointsManager.Instance.Points -= _points;
                Destroy(gameObject);
            }

        }
    }
}