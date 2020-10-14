using UnityEngine;
using Random = UnityEngine.Random;

namespace RhythmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MusicNote : MonoBehaviour
    {
        public enum Direction { Down, Left, Right, Up }
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _noteSprites;

        [SerializeField] private int _points = 10;
        [SerializeField] private float _spawnHitGap;
        [SerializeField] private float _spawnDespawnGap;

        [SerializeField] private FloatingScript _floatingTextPrefab;
        [SerializeField] private float _floatingTextOffsetX, _floatingTextOffsetY;
        //[SerializeField] private Vector2 _floatingTextSpawn = new Vector2(0, 5);

        private SongManager _songManager;
        private KeyCode _keyToHit;
        
        private Vector2 _spawnPos = new Vector2(0, 10);
        private Vector2 _hitPos = new Vector2(0, 5);
        private Vector2 _despawnPos = new Vector2(0, -5);
        private Vector2 _floatingTextSpawn = new Vector2(0, 5);
        
        public int Points => _points;
        public Vector2 SpawnPos => _spawnPos;
        public Vector2 DespawnPos => _despawnPos;

        public Vector2 HitPos
        {
            get => _hitPos;
            set
            {
                _hitPos = value;
                UpdatePoints();
            }
        }
        public Direction ThisDirection { get; private set; }


        private void Start()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();

            switch (Random.Range(1, 5))
            {
                case 1:
                    _spriteRenderer.sprite = _noteSprites[0];
                    ThisDirection = Direction.Down;
                    
                    break;
                
                case 2:
                    _spriteRenderer.sprite = _noteSprites[1];
                    ThisDirection = Direction.Left;
                    break;
                
                case 3:
                    _spriteRenderer.sprite = _noteSprites[2];
                    ThisDirection = Direction.Right;
                    break;
                
                case 4:
                    _spriteRenderer.sprite = _noteSprites[3];
                    ThisDirection = Direction.Up;
                    break;
            }
            
            _songManager = SongManager.Instance;
        }


        private void Update()
        {
            // Stop moving notes if music is not playing
            if(!_songManager.Audio.isPlaying) return;

            // Move note in time to song
            transform.position = Vector2.Lerp(
                _spawnPos,
                _despawnPos,
                (_songManager.BeatsShownInAdvance - (_songManager.Notes[_songManager.NextIndex - 1] - _songManager.SongPosInBeats)) / _songManager.BeatsShownInAdvance
            );

        }


        private void UpdatePoints()
        {
            _spawnPos.x = _hitPos.x;
            _spawnPos.y = _hitPos.y + _spawnHitGap;

            _despawnPos.x = _hitPos.x;
            _despawnPos.y = _hitPos.y - _spawnDespawnGap;

            _floatingTextSpawn.x = _hitPos.x + _floatingTextOffsetX;
            _floatingTextSpawn.y = _hitPos.y + _floatingTextOffsetY;
        }


        public void TriggerFloatingText(string message)
        {
            FloatingScript newFloatingText = Instantiate(_floatingTextPrefab, _floatingTextSpawn, Quaternion.identity);
            newFloatingText.SetString(message);
        }
    }
}