using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetValue(float value)
        {
            slider.value = value;
        }

        public void InitHealth()
        {
            slider.value = slider.maxValue;
        }
    }
}

