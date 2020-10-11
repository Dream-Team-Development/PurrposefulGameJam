using UnityEngine;

namespace RhythmGame
{
    public class SongManager : MonoBehaviour
    {
        public static SongManager Instance;
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private MusicNote[] _notePrefabs;
        [SerializeField] private float _bpm;
        [SerializeField] private int _beatsShownInAdvance = 3;
        [SerializeField] private float[] _notes;

        private int _nextIndex;
        // Current position of song (in seconds)
        private float _songPosition;
        // Current position of song (in beats)
        private float _songPosInBeats;
        // Duration of a beat (in seconds)
        private float _secPerBeat;
        // How time passed since song started (in seconds)
        private float _dspTimeSong;
        
        public AudioSource Audio => _audioSource;
        public int BeatsShownInAdvance => _beatsShownInAdvance;
        public float[] Notes => _notes;
        public int NextIndex => _nextIndex;
        public float SongPosInBeats => _songPosInBeats;

        
        private void Start()
        {
            Instance = GetComponent<SongManager>();
            
            if (!_audioSource) _audioSource = GetComponent<AudioSource>();
            
            // Calculate seconds in one beat
            _secPerBeat = 60f / _bpm;
            
            // Record time when song starts
            _dspTimeSong = (float) AudioSettings.dspTime;
            
            _audioSource.Play();
        }


        private void Update()
        {
            // Calculate current position (in seconds)
            _songPosition = (float) AudioSettings.dspTime - _dspTimeSong;

            // Calculate current position (in beats)
            _songPosInBeats = _songPosition / _secPerBeat;
            
            //Check end of song is reached or next note should be spawned
            if (_nextIndex < _notes.Length && _notes[_nextIndex] < _songPosInBeats + _beatsShownInAdvance)
            {
                if (_notePrefabs.Length != 0)
                {
                    // 0 == Player 1, 1 == Player 2
                    Instantiate(_notePrefabs[0], _notePrefabs[0].SpawnPos, Quaternion.identity);
                    Instantiate(_notePrefabs[1], _notePrefabs[1].SpawnPos, Quaternion.identity);
                }
                
                _nextIndex++;
            }

            if(_nextIndex >= _notes.Length) _audioSource.Stop();
        }
    }
}
