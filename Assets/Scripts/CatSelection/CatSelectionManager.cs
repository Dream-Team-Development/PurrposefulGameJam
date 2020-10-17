using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CatSelection
{
    public class CatSelectionManager : MonoBehaviour
    {
        public static CatSelectionManager Instance;
        
        [SerializeField] private CatObject[] _catOptions;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button[] _selectButtons;
        [SerializeField] private CatCardManager _p1CatCard, _p2CatCard;
        [SerializeField] private float _startingWeightModifyer;

        private CatObject[] _p1Options, _p2Options;
        private int _p1Index, _p2Index;
        private int _p1CatChoice, _p2CatChoice;
        private bool _p1Selected, _p2Selected;


        private void Start()
        {
            Instance = GetComponent<CatSelectionManager>();
            
            _playButton.enabled = false;

            _p1Options = _catOptions; _p1Index = 0;
            _p2Options = _catOptions; _p2Index = 0;
            
            UpdateCatCard(1);
            UpdateCatCard(2);
        }


        private void Update()
        {
            if (_p1Selected && _p2Selected) _playButton.enabled = true;
        }


        public void RotateRight(int player)
        {
            switch (player)
            {
                case 1:
                    if (_p1Index+1 == _p1Options.Length) _p1Index = 0;
                    else _p1Index++;
                    UpdateCatCard(1);
                    break;
                
                case 2:
                    if (_p2Index+1 == _p2Options.Length) _p2Index = 0;
                    else _p2Index++;
                    UpdateCatCard(2);
                    break;
            }
        }


        public void RotateLeft(int player)
        {
            switch (player)
            {
                case 1:
                    if (_p1Index-1 == -1) _p1Index = _p1Options.Length - 1;
                    else _p1Index--;
                    UpdateCatCard(1);
                    break;
                
                case 2:
                    if (_p2Index-1 == -1) _p2Index = _p2Options.Length - 1;
                    else _p2Index--;
                    UpdateCatCard(2);
                    break;
            }
        }


        private void UpdateCatCard(int player)
        {
            if (!_p1CatCard || !_p2CatCard) return;
            
            switch (player)
            {
                case 1:
                    _p1CatCard.CatImage.sprite = _p1Options[_p1Index].profileImage;
                    _p1CatCard.CatBreedText.text = "Breed: " + _p1Options[_p1Index].catName;
                    _p1CatCard.CatWeightText.text = "Goal Weight: " + _p1Options[_p1Index].idealWeight;
                    _p1CatCard.CatSexImage.sprite = _p1Options[_p1Index].sexImage;
                    
                    if(_p1Selected && _p1Index == _p1CatChoice)
                        _p1CatCard.CatSelected.SetActive(true);
                    else
                        _p1CatCard.CatSelected.SetActive(false);

                    if (_p2Selected && _p1Index == _p2CatChoice)
                    {
                        _p1CatCard.BackgroundImage.color = Color.gray;
                        _selectButtons[0].enabled = false;
                    }
                    else
                    {
                        _p1CatCard.BackgroundImage.color = Color.white;
                        _selectButtons[0].enabled = true;
                    }
                    
                    break;
                
                case 2:
                    _p2CatCard.CatImage.sprite = _p2Options[_p2Index].profileImage;
                    _p2CatCard.CatBreedText.text = "Breed: " + _p2Options[_p2Index].catName;
                    _p2CatCard.CatWeightText.text = "Goal Weight: " + _p2Options[_p2Index].idealWeight;
                    _p2CatCard.CatSexImage.sprite = _p2Options[_p2Index].sexImage;
                    
                    if(_p2Selected && _p2Index == _p2CatChoice)
                        _p2CatCard.CatSelected.SetActive(true);
                    else
                        _p2CatCard.CatSelected.SetActive(false);

                    if (_p1Selected && _p2Index == _p1CatChoice)
                    {
                        _p2CatCard.BackgroundImage.color = Color.gray;
                        _selectButtons[1].enabled = false;
                    }
                    else
                    {
                        _p2CatCard.BackgroundImage.color = Color.white;
                        _selectButtons[1].enabled = true;
                    }
                    
                    break;
            }
        }


        public void SelectCat(int player)
        {
            switch (player)
            {
                case 1:
                    _p1CatChoice = _p1Index;
                    _p1Selected = true;
                    break;
                
                case 2:
                    _p2CatChoice = _p2Index;
                    _p2Selected = true;
                    break;
            }
            
            UpdateCatCard(1);
            UpdateCatCard(2);
        }


        public void LetTheGamesBegin(string sceneName)
        {
            Debug.Log("Let the games begin");

            PlayerPrefs.SetInt("P1Cat", _p1CatChoice); 
            PlayerPrefs.SetInt("P2Cat", _p2CatChoice);
            
            PlayerPrefs.SetFloat("P1Weight", _p1Options[_p1CatChoice].idealWeight + _startingWeightModifyer);
            PlayerPrefs.SetFloat("P2Weight", _p2Options[_p2CatChoice].idealWeight + _startingWeightModifyer);
        
            PlayerPrefs.SetFloat("P1Energy", 20);
            PlayerPrefs.SetFloat("P2Energy", 20);

            SceneManager.LoadScene(sceneName);
        }
    }
}
