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
        private enum Direction { Down, Left, Right, Up }
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _noteSprites;

        [SerializeField] private Player _thisPlayer = Player.One;
        [SerializeField] private int _points = 10;
        [SerializeField] private Vector2 _spawnPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _hitPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _despawnPos = new Vector2(0, -5);

        [SerializeField] private GameObject _floatingTextPrefab;
        [SerializeField] private Vector2 _floatingTextSpawn = new Vector2(0, 5);

        private SongManager _songManager;
        private Direction _thisDirection;

        public Vector2 SpawnPos => _spawnPos;
        public GameObject HitPos { get; set; }


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

            switch (_thisDirection)
            {
                case Direction.Down:
                    CheckHit(_thisPlayer == Player.One ? KeyCode.S : KeyCode.DownArrow);
                    break;
                
                case Direction.Left:
                    CheckHit(_thisPlayer == Player.One ? KeyCode.A : KeyCode.LeftArrow);
                    break;
                
                case Direction.Right:
                    CheckHit(_thisPlayer == Player.One ? KeyCode.D : KeyCode.RightArrow);
                    break;
                
                case Direction.Up:
                    CheckHit(_thisPlayer == Player.One ? KeyCode.W : KeyCode.UpArrow);
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

            if (transform.position.y >= _despawnPos.y && transform.position.y <= _hitPos.y - 2)
            {
                GuiManager.Instance.P1Weight -= _points;
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
                switch (_thisPlayer)
                {
                    case Player.One:
                        GuiManager.Instance.P1Weight += _points;
                        TriggerFloatingText("Nice!");
                        break;
                    case Player.Two:
                        GuiManager.Instance.P2Weight += _points;
                        TriggerFloatingText("Nice!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (!hit)
            {
                switch (_thisPlayer)
                {
                    case Player.One:
                        GuiManager.Instance.P1Weight -= _points / 2;
                        TriggerFloatingText("Missed!");
                        break;
                    case Player.Two:
                        GuiManager.Instance.P2Weight -= _points / 2;
                        TriggerFloatingText("Missed!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            Destroy(gameObject);
        }


        void TriggerFloatingText(string message)
        {
            GameObject newFloatingText = Instantiate(_floatingTextPrefab, _floatingTextSpawn, Quaternion.identity);
            newFloatingText.GetComponent<TextMesh>().text = message;
        }
    }
}