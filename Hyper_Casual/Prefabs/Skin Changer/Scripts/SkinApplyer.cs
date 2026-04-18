using SanyaBeerExtension;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MediaKit_M.SkinChanger
{
    public class SkinApplyer : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _boughtButton;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private Button _canSelectButton;

        [Header("Tabs")]
        [SerializeField] private TabSelecter _tabSelecter;

        [Inject] private ISkinSaveLoader _loader;

        private void OnEnable()
        {
            _boughtButton.onClick.AddListener(OnBuy);
            _selectedButton.onClick.AddListener(OnSelect);
            _canSelectButton.onClick.AddListener(OnCanSelect);

            foreach (var tab in _tabSelecter.Tabs)
            {
                foreach (var skinItem in tab.SkinItems)
                    skinItem.OnClicked += OnSelectItem;
            }
        }

        private void OnDisable()
        {
            _boughtButton.onClick.RemoveListener(OnBuy);
            _selectedButton.onClick.RemoveListener(OnSelect);
            _canSelectButton.onClick.RemoveListener(OnCanSelect);

            foreach (var tab in _tabSelecter.Tabs)
            {
                foreach (var skinItem in tab.SkinItems)
                    skinItem.OnClicked -= OnSelectItem;
            }
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

        private void OnSelectItem(SkinItem item)
        {
            if (item.IsUnlocked == true && item.IsSelected == true)
            {
                ShowSelectedButton();
                return;
            }

            if (item.IsUnlocked == true)
            {
                ShowCanSelectButton();
                return;
            }

            ShowBoughtButton();
        }

        private void ShowBoughtButton()
        {
            _boughtButton.ActiveSelf();
            _selectedButton.DisactiveSelf();
            _canSelectButton.DisactiveSelf();
        }

        private void ShowSelectedButton()
        {
            _boughtButton.DisactiveSelf();
            _selectedButton.ActiveSelf();
            _canSelectButton.DisactiveSelf();
        }

        private void ShowCanSelectButton()
        {
            _boughtButton.DisactiveSelf();
            _selectedButton.DisactiveSelf();
            _canSelectButton.ActiveSelf();
        }
    }
}