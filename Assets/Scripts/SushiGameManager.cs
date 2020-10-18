using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class SushiGameManager : MonoBehaviour
{
     public static SushiGameManager Instance;
     
     [SerializeField] protected float _playTime;
     [SerializeField] protected GameObject _startInfo, _endGame;
     [SerializeField] protected Text _countdownText;
     [SerializeField] private AudioClip _song;

     protected float _timeLeft, _startCountdown, _endCountdown;
     protected bool _canStart, _playing;


     public bool Playing => _playing;


     protected virtual void Start()
     {
          Instance = GetComponent<SushiGameManager>();
          
          SoundManager.Instance.ChangeTheTrack(_song);
          SoundManager.Instance.PlayMusic();
          
          if(_startInfo) _startInfo.SetActive(true);
          if(_endGame) _endGame.SetActive(false);
          if(_countdownText) _countdownText.gameObject.SetActive(false);
          
          _timeLeft = _playTime;

          _canStart = false;
          
          _playing = false;
          _startCountdown = 3f;

          _endCountdown = 5f;
     }

     
     private void Update()
     {
          if (_canStart && !_playing)
          {
               StartingCountdown();
               return;
          }

          if (!_playing) return;

          _timeLeft -= Time.deltaTime;
          
          if(_timeLeft <= 5)
          {
               _countdownText.gameObject.SetActive(true);
               EndingCountdown();
          }
          else if(_timeLeft <= 0)
               GameOver();
     }


     public virtual void TriggerStart()
     {
          if(_startInfo) _startInfo.SetActive(false);
          _canStart = true;
          if(_countdownText) _countdownText.gameObject.SetActive(true);
     }


     protected virtual void StartingCountdown()
     {
          _startCountdown -= Time.deltaTime;
          _countdownText.text = Math.Ceiling(_startCountdown).ToString();

          if (_startCountdown <= 0)
          {
               _countdownText.text = "GO!";
          }
          
          if (_startCountdown <= -1)
          {
               _countdownText.gameObject.SetActive(false);
               _playing = true;
          }
     }


     private void EndingCountdown()
     {
          _endCountdown -= Time.deltaTime;
          _countdownText.text = Math.Ceiling(_endCountdown).ToString();
          
          if (_endCountdown <= 0)
          {
               GameOver();
          }
     }

     
     protected virtual void GameOver()
     {
          PlayerManagerSushi.Instance.SaveStats();
          
          if(_countdownText) _countdownText.gameObject.SetActive(false);
          if(_endGame) _endGame.SetActive(true);
          _playing = false;
     }
}
