using UnityEngine;
using UnityEngine.UI;

namespace CatSelection
{
    public class CatSelectionManager : MonoBehaviour
    {
        public static CatSelectionManager Instance;
        
        [SerializeField] private CatObject[] _catOptions;
        [SerializeField] private Button _playButton;
        [SerializeField] private CatCardManager _p1CatCard, _p2CatCard;

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
                    _p1CatCard.CatPic.sprite = _p1Options[_p1Index].profileImage;
                    _p1CatCard.CatBreedText.text = "Breed: " + _p1Options[_p1Index].catName;
                    _p1CatCard.CatWeightText.text = "Goal Weight: " + _p1Options[_p1Index].idealWeight;
                    _p1CatCard.CatSexText.text = "Sex: " + _p1Options[_p1Index].sex;
                    break;
                
                case 2:
                    _p2CatCard.CatPic.sprite = _p2Options[_p2Index].profileImage;
                    _p2CatCard.CatBreedText.text = "Breed: " + _p2Options[_p2Index].catName;
                    _p2CatCard.CatWeightText.text = "Goal Weight: " + _p2Options[_p2Index].idealWeight;
                    _p2CatCard.CatSexText.text = "Sex: " + _p2Options[_p2Index].sex;
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
                    Debug.Log("P1 selected " + _p1Options[_p1CatChoice].catName);
                    break;
                
                case 2:
                    _p2CatChoice = _p2Index;
                    _p1Selected = true;
                    Debug.Log("P2 selected " + _p2Options[_p2CatChoice].catName);
                    break;
            }
        }


        public void LetTheGamesBegin()
        {
            Debug.Log("Let the games begin");
        }
    }
}
