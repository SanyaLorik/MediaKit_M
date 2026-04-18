using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MediaKit_M.SkinChanger
{
    public class SkinApplyer : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _boughtButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _canSelectButton;

        [Inject] private ISkinSaveLoader _loader;

        private void OnEnable()
        {
            _boughtButton.onClick.AddListener(OnBuy);
            _boughtButton.onClick.AddListener(OnSelect);
            _boughtButton.onClick.AddListener(OnCanSelect);
        }

        private void OnDisable()
        {
            _boughtButton.onClick.RemoveListener(OnBuy);
            _boughtButton.onClick.RemoveListener(OnSelect);
            _boughtButton.onClick.RemoveListener(OnCanSelect);
        }

        private void OnBuy()
        {

        }

        private void OnSelect()
        {

        }

        private void OnCanSelect()
        {

        }
    }
}