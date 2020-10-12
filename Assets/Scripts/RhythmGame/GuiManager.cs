using UnityEngine;
using UnityEngine.UI;

namespace RhythmGame
{
    public class GuiManager : MonoBehaviour
    {
        public static GuiManager Instance;
        
        [SerializeField] private Text _p1WeightText;
        [SerializeField] private Text _p2WeightText;

        private int _p1Weight;
        private int _p2Weight;

        public int P1Weight
        {
            get => _p1Weight;
            set
            {
                _p1Weight = value;
                _p1WeightText.text = "Points: " + _p1Weight;
            }
        }

        public int P2Weight
        {
            get => _p2Weight;
            set
            {
                _p2Weight = value;
                _p2WeightText.text = "Points: " + _p2Weight;
            }
        }


        private void Start()
        {
            Instance = GetComponent<GuiManager>();
        }
    }
}
