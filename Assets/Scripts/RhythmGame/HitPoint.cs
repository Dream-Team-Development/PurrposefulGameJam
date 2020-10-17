using System;
using UnityEngine;

namespace RhythmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HitPoint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }

        private void Start()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
