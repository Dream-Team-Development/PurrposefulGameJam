using System;
using UnityEngine;

namespace RhythmGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MusicNote _musicNote;
        [SerializeField] private GameObject _hitPoint;

        private void Start()
        {
            _musicNote.HitPos = _hitPoint;
        }
    }
}
