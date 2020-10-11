using UnityEngine;
using UnityEngine.UI;

namespace RhythmGame
{
    public class PointsManager : MonoBehaviour
    {
        public static PointsManager Instance;
        
        [SerializeField] private Text _p1PointsText;
        [SerializeField] private Text _p2PointsText;

        private int _p1Points;
        private int _p2Points;

        public int P1Points
        {
            get => _p1Points;
            set
            {
                _p1Points = value;
                _p1PointsText.text = "Points: " + _p1Points;
            }
        }

        public int P2Points
        {
            get => _p2Points;
            set
            {
                _p2Points = value;
                _p2PointsText.text = "Points: " + _p2Points;
            }
        }


        private void Start()
        {
            Instance = GetComponent<PointsManager>();
        }
    }
}
