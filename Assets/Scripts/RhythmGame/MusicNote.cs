using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RhythmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MusicNote : MonoBehaviour
    {
        private enum Player { One, Two }
        public enum Direction { Down, Left, Right, Up }
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _noteSprites;

        [SerializeField] private Player _thisPlayer = Player.One;
        [SerializeField] private int _points = 10;
        [SerializeField] private Vector2 _spawnPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _hitPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _despawnPos = new Vector2(0, -5);

        [SerializeField] private FloatingScript _floatingTextPrefab;
        [SerializeField] private Vector2 _floatingTextSpawn = new Vector2(0, 5);

        private SongManager _songManager;
        private Direction _thisDirection;
        private KeyCode _keyToHit;
        
        public int Points => _points;
        public Vector2 SpawnPos => _spawnPos;

        public Vector2 HitPos
        {
            get => _hitPos;
            set => _hitPos = value;
        }
        
        public Direction ThisDirection => _thisDirection;


        private void Start()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();

            switch (Random.Range(1, 5))
            {
                case 1:
                    _spriteRenderer.sprite = _noteSprites[0];
                    _thisDirection = Direction.Down;
                    
                    break;
                
                case 2:
                    _spriteRenderer.sprite = _noteSprites[1];
                    _thisDirection = Direction.Left;
                    break;
                
                case 3:
                    _spriteRenderer.sprite = _noteSprites[2];
                    _thisDirection = Direction.Right;
                    break;
                
                case 4:
                    _spriteRenderer.sprite = _noteSprites[3];
                    _thisDirection = Direction.Up;
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

            if (transform.position.y >= _despawnPos.y && transform.position.y <= _hitPos.y - 2)
            {
                if(_thisPlayer == Player.One) GuiManager.Instance.P1Weight -= _points;
                else GuiManager.Instance.P2Weight -= _points;
                
                Destroy(gameObject);
            }

        }


        public void TriggerFloatingText(string message)
        {
            FloatingScript newFloatingText = Instantiate(_floatingTextPrefab, _floatingTextSpawn, Quaternion.identity);
            newFloatingText.SetString(message);
        }
    }
}