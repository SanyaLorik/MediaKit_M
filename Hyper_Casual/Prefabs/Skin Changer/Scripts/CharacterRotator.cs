using SanyaBeerExtension;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    public class CharacterRotator : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] private Transform _rotating;

        [Header("Input")]
        [SerializeField] private Slider _reviewSlider;

        const float _reviewSliderDefaultValue = 0.5f;

        private void OnEnable()
        {
            InitializeDefault();

            _reviewSlider.onValueChanged.AddListener(OnRotate);
        }

        private void OnDisable()
        {
            _reviewSlider.onValueChanged.RemoveListener(OnRotate);
        }

        private void OnRotate(float value)
        {
            Rotate(value);
        }

        private void InitializeDefault()
        {
            _reviewSlider.value = _reviewSliderDefaultValue;
            Rotate(_reviewSlider.value);
        }

        private void Rotate(float value)
        {
            float from = -180;
            float to = -360 + from;

            float angleY = Mathf.Lerp(from, to, value);
            Vector3 angle = Vector3.zero.SetY(angleY);

            SetAngle(angle);
        }

        private void SetAngle(Vector3 angle)
        {
            _rotating.localEulerAngles = angle;
        }
    }
}