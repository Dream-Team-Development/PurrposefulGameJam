using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _song;
    [SerializeField] private SoundManager _soundManager;

    private void Start()
    {
        _soundManager.ChangeTheTrack(_song);
        _soundManager.PlayMusic();
    }
}
