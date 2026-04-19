using SanyaBeerExtension;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    [Serializable]
    public class SkinApplyer
    {
        [Header("Buttons")]
        [SerializeField] private Button _boughtButton;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private Button _canSelectButton;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _boughtText;

        [Header("Purchase")]
        [SerializeField] private PurchaseAdapter _purchaseAdapter;

        private TabSelecter _tabSelecter;
        private SkinCollection _skinCollection;
        private NumberFormatter _numberFormatter;

        private SkinItem _currentSkin;

        public void Initialize(TabSelecter tabSelecter, SkinCollection skinCollection, NumberFormatter numberFormatter)
        {
            _tabSelecter = tabSelecter;
            _skinCollection = skinCollection;
            _numberFormatter = numberFormatter;
        }

        public void SetupInitial()
        {
            SelectItem(_tabSelecter.CurrentTab.SkinItems[0]);
            _currentSkin = _skinCollection.CurrentSkin;
        }

        public void Enable()
        {
            _boughtButton.onClick.AddListener(OnBuy);
            _selectedButton.onClick.AddListener(OnSelect);
            _canSelectButton.onClick.AddListener(OnCanSelect);

            _tabSelecter.OnSelected += OnSelectItemViaTab;

            foreach (var tab in _tabSelecter.Tabs)
            {
                foreach (var skinItem in tab.SkinItems)
                    skinItem.OnClicked += OnSelectItem;
            }
        }

        public void Disable()
        {
            _boughtButton.onClick.RemoveListener(OnBuy);
            _selectedButton.onClick.RemoveListener(OnSelect);
            _canSelectButton.onClick.RemoveListener(OnCanSelect);

            _tabSelecter.OnSelected -= OnSelectItemViaTab;

            foreach (var tab in _tabSelecter.Tabs)
            {
                foreach (var skinItem in tab.SkinItems)
                    skinItem.OnClicked -= OnSelectItem;
            }

            _currentSkin?.StopSelectAnimation();
        }

        private void OnBuy()
        {
            Buy();
        }

        private void OnSelect()
        {

        }

        private void OnCanSelect()
        {
            _skinCollection.SetCurrentWear(_currentSkin);
            ShowSelectedButton();
            ReloadAnimation(_currentSkin);
        }

        private void OnSelectItem(SkinItem item)
        {
            SelectItem(item);
        }

        private void OnSelectItemViaTab(Tab tab)
        {
            SelectItem(tab.SkinItems[0]);
        }

        private void SelectItem(SkinItem item)
        {
            ReloadAnimation(item);

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

            bool isInteractable = _purchaseAdapter.CanSpend(item.Data.Money);
            ShowBoughtButton(isInteractable, item.Data.Money);
        }

        private void Buy()
        {
            _skinCollection.AddWear(_currentSkin);
            _skinCollection.SetCurrentWear(_currentSkin);

            _purchaseAdapter.Spend(_currentSkin.Data.Money);

            ShowSelectedButton();
        }

        private void ShowBoughtButton(bool isInteractable = true, int money = 777)
        {
            _boughtButton.ActiveSelf();
            _selectedButton.DisactiveSelf();
            _canSelectButton.DisactiveSelf();

            _boughtButton.interactable = isInteractable;
            _boughtText.text = _numberFormatter.ValuteFormatter(money);
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

        private void ReloadAnimation(SkinItem item)
        {
            _currentSkin?.StopSelectAnimation();
            _currentSkin = item;
            _currentSkin.StartSelectAnimation();
        }
    }
}