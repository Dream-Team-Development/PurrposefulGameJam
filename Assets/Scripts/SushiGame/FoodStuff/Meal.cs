using SushiGame.Controller;
using UnityEngine;

namespace SushiGame.FoodStuff
{
    public class Meal : AiAgent, IInteract
    {
        [Header("Interact")]
        [SerializeField] private float _weightGain;
        [SerializeField] private float _energyGain;
        [SerializeField] private float _sicknessGain;
        public void Interact(PlayerController controller)
        {
            controller.Weight += _weightGain;
            controller.Energy += _energyGain;
            controller.SicknessLevel += _sicknessGain;
            gameObject.SetActive(false);
        }
    }
}
