using System;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    private enum Result { Player1, Player2, Tie }

    [SerializeField] private CatObject[] _cats;
    [SerializeField] private AudioClip _song;

    [Header("UI")]
    [SerializeField] private Text[] _winnerText;
    [SerializeField] private Image _winnerImage;
    [SerializeField] private Text _winnerBreed, _winnerEndWeight, _winnerGoalWeight, _winnerFact;
    [SerializeField] private Image _loserImage;
    [SerializeField] private Text _loserBreed, _loserEndWeight, _loserGoalWeight, _loserFact;

    private Result _result;
    private CatObject _p1Cat, _p2Cat;
    private float _p1EndWeight, _p2EndWeight;
    private float _p1GoalDiff, _p2GoalDiff;
    
    private void Start()
    {
        SoundManager.Instance.ChangeTheTrack(_song);
        SoundManager.Instance.PlayMusic();
        
        _p1Cat = _cats[PlayerPrefs.GetInt("P1Cat")];
        _p2Cat = _cats[PlayerPrefs.GetInt("P2Cat")];
        
        _p1EndWeight = PlayerPrefs.GetFloat("P1Weight");
        _p2EndWeight = PlayerPrefs.GetFloat("P2Weight");

        _p1GoalDiff = _p1EndWeight - _p1Cat.idealWeight;
        _p2GoalDiff = _p2EndWeight - _p2Cat.idealWeight;

        if (_p1GoalDiff == _p2GoalDiff)
            _result = Result.Tie;
        
        else if (_p1GoalDiff < _p2GoalDiff)
            _result = Result.Player1;
        
        else if (_p2GoalDiff < _p1GoalDiff)
            _result = Result.Player2;
        
        SetUI();
    }


    private void SetUI()
    {
        switch (_result)
        {
            case Result.Player1:
                PlayerOneWins();
                break;
            
            case Result.Player2:
                PlayerTwoWins();
                break;
            
            case Result.Tie:
                Tie();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlayerOneWins()
    {
        foreach (Text text in _winnerText)
        {
            text.text = "Player One wins!";
        }

        _winnerImage.sprite = _p1Cat.winImage;
        _winnerBreed.text = "Breed: " + _p1Cat.catName;
        _winnerEndWeight.text = "Final weight: " + (Mathf.Round(_p1EndWeight * 10f) / 10f) + "kg";
        _winnerGoalWeight.text = "Goal weight: " + _p1Cat.idealWeight + "kg";
        _winnerFact.text = "Fact: " + _p1Cat.fact;

        _loserImage.sprite = _p2Cat.loseImage;
        _loserBreed.text = "Breed: " + _p2Cat.catName;
        _loserEndWeight.text = "Final weight: " + (Mathf.Round(_p2EndWeight * 10f) / 10f) + "kg";
        _loserGoalWeight.text = "Goal weight: " +_p2Cat.idealWeight + "kg";
        _loserFact.text = "Fact: " + _p2Cat.fact;

    }

    private void PlayerTwoWins()
    {
        foreach (Text text in _winnerText)
        {
            text.text = "Player Two wins!";
        }

        _winnerImage.sprite = _p2Cat.winImage;
        _winnerBreed.text = "Breed: " + _p2Cat.catName;
        _winnerEndWeight.text = "Final weight: " + (Mathf.Round(_p2EndWeight * 10f) / 10f) + "kg";
        _winnerGoalWeight.text = "Goal weight: " +_p2Cat.idealWeight + "kg";
        _winnerFact.text = "Fact: " + _p2Cat.fact;

        _loserImage.sprite = _p1Cat.loseImage;
        _loserBreed.text = "Breed: " + _p1Cat.catName;
        _loserEndWeight.text = "Final weight: " + (Mathf.Round(_p1EndWeight * 10f) / 10f) + "kg";
        _loserGoalWeight.text = "Goal weight: " +_p1Cat.idealWeight + "kg";
        _loserFact.text = "Fact: " + _p1Cat.fact;

    }

    private void Tie()
    {
        foreach (Text text in _winnerText)
        {
            text.text = "It's a tie!";
        }

        _winnerImage.sprite = _p1Cat.winImage;
        _winnerBreed.text = "Breed: " + _p1Cat.catName;
        _winnerEndWeight.text = "Final weight: " + (Mathf.Round(_p1EndWeight * 10f) / 10f) + "kg";
        _winnerGoalWeight.text = "Goal weight: " +_p1Cat.idealWeight + "kg";
        _winnerFact.text = "Fact: " + _p1Cat.fact;

        _loserImage.sprite = _p2Cat.winImage;
        _loserBreed.text = "Breed: " + _p2Cat.catName;
        _loserEndWeight.text = "Final weight: " + (Mathf.Round(_p2EndWeight * 10f) / 10f) + "kg";
        _loserGoalWeight.text = "Goal weight: " +_p2Cat.idealWeight + "kg";
        _loserFact.text = "Fact: " + _p2Cat.fact;

    }
}
