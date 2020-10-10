using System;
using UnityEngine;

namespace RhythmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MusicNote : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Vector2 _spawnPos = new Vector2(0, 5);
        [SerializeField] private Vector2 _removePos = new Vector2(0, -5);

        private SongManager _songManager;

        public Vector2 SpawnPos => _spawnPos;
        public Vector2 RemovePos => _removePos;


        private void Start()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _songManager = SongManager.Instance;
        }


        private void Update()
        {
            if(!_songManager.Audio.isPlaying) return;

            transform.position = Vector2.Lerp(
                _spawnPos,
                _removePos,
                (_songManager.BeatsShownInAdvance - (_songManager.Notes[_songManager.NextIndex - 1] - _songManager.SongPosInBeats)) / _songManager.BeatsShownInAdvance
            );
            
            if(transform.position.y == _removePos.y) Destroy(this);

        }
    }
}