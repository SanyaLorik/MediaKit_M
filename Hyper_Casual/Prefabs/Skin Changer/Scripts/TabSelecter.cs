using SanyaBeerExtension;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    public class TabSelecter : MonoBehaviour
    {
        [Header("Skins")]
        [SerializeField] private Tab[] _tabs;

        [Header("Content For Scroll")]
        [SerializeField] private ScrollRect _scrollRect;

        public event Action<Tab> OnSelected = delegate { };

        private Tab _currentTab;

        private void Awake()
        {
            _currentTab = _tabs[0];
            ShowTab(_currentTab);
        }

        private void OnEnable()
        {
            _tabs.ForEachs(tab => tab.Initialize(), tab => tab.OnClick += OnShowTab);
        }

        private void OnDisable()
        {
            _tabs.ForEachs(tab => tab.Deinitialize(), tab => tab.OnClick -= OnShowTab);
        }

        private void OnShowTab(Tab tab)
        {
            ShowTab(tab);
        }

        private void ShowTab(Tab tab)
        {
            _currentTab.Hide();
            _currentTab = tab;
            _currentTab.Show();

            _scrollRect.content = _currentTab.RectTab;

            OnSelected.Invoke(_currentTab);
        }
    }
}