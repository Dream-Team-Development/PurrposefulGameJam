using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        Instance = GetComponent<SoundManager>();
        
        if (!_audio) _audio = GetComponent<AudioSource>();
        
        DontDestroyOnLoad(this);
    }

    public void PlayMusic()
    {
        _audio.Play();
    }

    public void StopMusic()
    {
        _audio.Stop();
    }

    public void ChangeTheTrack(AudioClip newClip)
    {
        _audio.clip = newClip;
    }
}
