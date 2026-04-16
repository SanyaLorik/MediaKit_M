using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    public class CharacterRotator : MonoBehaviour
    {
        [SerializeField] private Transform _skinContainer;

        [Header("Input")]
        [SerializeField] private Slider _reviewSlider;

        private void OnEnable()
        {
            InitializeDefualt();
        }

        private void InitializeDefualt()
        {
            const float reviewSliderValue = 0.5f;
            _reviewSlider.value = reviewSliderValue;
        }

        private void Rotate()
        {
            //_skinContainer
        }
    }
}