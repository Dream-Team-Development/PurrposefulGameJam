using UnityEngine;
using UnityEngine.UI;

namespace RhythmGame
{
    public class PointsManager : MonoBehaviour
    {
        public static PointsManager Instance;
        
        [SerializeField] private Text _pointsText;

        private int _points;

        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                _pointsText.text = "Points: " + _points;
            }
        }


        private void Start()
        {
            Instance = GetComponent<PointsManager>();
        }
    }
}
