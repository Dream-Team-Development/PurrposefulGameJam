using System;
using RhythmGame;
using UnityEngine;

public class RhythmGameManager : SushiGameManager
{
    public new static RhythmGameManager Instance;
    
    protected override void Start()
    {
        Instance = GetComponent<RhythmGameManager>();
        
        SoundManager.Instance.StopMusic();
          
        if(_startInfo) _startInfo.SetActive(true);
        if(_endGame) _endGame.SetActive(false);
        if(_countdownText) _countdownText.gameObject.SetActive(false);
          
        _timeLeft = _playTime;

        _canStart = false;
          
        _playing = false;
        _startCountdown = 3f;

        _endCountdown = 5f;
    }
    
    
    public override void TriggerStart()
    {
        _canStart = true;
        _countdownText.gameObject.SetActive(true);
    }


    protected override void StartingCountdown()
    {
        _startCountdown -= Time.deltaTime;
        _countdownText.text = Math.Ceiling(_startCountdown).ToString();

        if (_startCountdown <= 0)
        {
            _countdownText.text = "GO!";
        }

        if (!(_startCountdown <= -1)) return;
        
        if(_countdownText) _countdownText.gameObject.SetActive(false);
        _playing = true;
        SongManager.Instance.StartGame();
    }

     
    protected override void GameOver()
    {
        PlayerManagerRhythm.Instance.SaveStats();
          
        if(_countdownText) _countdownText.gameObject.SetActive(false);
        if(_endGame) _endGame.SetActive(true);
        _playing = false;
    }
}
