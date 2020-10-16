using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameManager : MonoBehaviour
{
     public static BaseGameManager Instance;
     
     [SerializeField] private float _playTime;
     [SerializeField] private GameObject _startInfo, _endGame;
     [SerializeField] private Text _countdownText;

     private float _timeLeft, _startCountdown, _endCountdown;
     private bool _canStart, _playing;


     public bool Playing => _playing;


     private void Start()
     {
          Instance = GetComponent<BaseGameManager>();
          
          _startInfo.SetActive(true);
          _endGame.SetActive(false);
          _countdownText.gameObject.SetActive(false);
          
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


     public void TriggerStart()
     {
          _startInfo.SetActive(false);
          _canStart = true;
          _countdownText.gameObject.SetActive(true);
     }


     private void StartingCountdown()
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

     
     private void GameOver()
     {
          _countdownText.gameObject.SetActive(false);
          _endGame.SetActive(true);
          _playing = false;
     }
}
