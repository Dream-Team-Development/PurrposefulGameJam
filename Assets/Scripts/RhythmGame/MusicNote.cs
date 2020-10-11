using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RhythmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MusicNote : MonoBehaviour
    {
        private enum Direction { Down, Left, Right, Up }
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _noteSprites;

        [SerializeField] private int _points = 10;
        [SerializeField] private Vector2 _spawnPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _hitPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _despawnPos = new Vector2(0, -5);

        private SongManager _songManager;
        private Direction _thisDirection;

        public Vector2 SpawnPos => _spawnPos;


        private void Start()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();

            switch (Random.Range(1, 4))
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

            switch (_thisDirection)
            {
                case Direction.Down:
                    CheckHit(KeyCode.DownArrow);
                    break;
                
                case Direction.Left:
                    CheckHit(KeyCode.LeftArrow);
                    break;
                
                case Direction.Right:
                    CheckHit(KeyCode.RightArrow);
                    break;
                
                case Direction.Up:
                    CheckHit(KeyCode.UpArrow);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Move note in time to song
            transform.position = Vector2.Lerp(
                _spawnPos,
                _despawnPos,
                (_songManager.BeatsShownInAdvance - (_songManager.Notes[_songManager.NextIndex - 1] - _songManager.SongPosInBeats)) / _songManager.BeatsShownInAdvance
            );

            if (transform.position.y <= _despawnPos.y)
            {
                PointsManager.Instance.Points -= _points;
                Destroy(gameObject);
            }

        }


        private void CheckHit(KeyCode keyToHit)
        {
            foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (!Input.GetKeyDown(keyCode)) continue;
                
                if (keyCode != keyToHit)
                    Hit(false);
                    
                else if (keyCode == keyToHit)
                {
                    if (transform.position.y <= _hitPos.y + 1 && transform.position.y >= _hitPos.y - 1)
                        Hit(true);
                    
                    else
                        Hit(false);
                }
            }
        }


        private void Hit(bool hit)
        {
            if (hit)
            {
                PointsManager.Instance.Points += _points;
            }
            else if (!hit)
            {
                PointsManager.Instance.Points -= _points / 2;
            }
            
            
            Destroy(gameObject);
        }
    }
}