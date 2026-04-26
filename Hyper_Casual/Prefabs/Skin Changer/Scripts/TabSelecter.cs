using SanyaBeerExtension;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    [Serializable]
    public class TabSelecter
    {
        [Header("Skins")]
        [SerializeField] private Tab[] _tabs;

        [Header("Content For Scroll")]
        [SerializeField] private ScrollRect _scrollRect;

        [Header("View")]
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;

        public event Action<Tab> OnSelected = delegate { };

        public IReadOnlyCollection<Tab> Tabs => _tabs;

        public Tab CurrentTab { get; private set; }

        public void SetupInitial()
        {
            CurrentTab = _tabs[0];
            ShowTab(CurrentTab);
        }

        public void Enable()
        {
            _tabs.ForEachs(tab => tab.Initialize(), tab => tab.OnClick += OnShowTab);
        }

        public void Disable()
        {
            _tabs.ForEachs(tab => tab.Deinitialize(), tab => tab.OnClick -= OnShowTab);
        }

        private void OnShowTab(Tab tab)
        {
            ShowTab(tab);
        }

        private void ShowTab(Tab tab)
        {
            CurrentTab.Hide();
            CurrentTab.SetButtonColor(_unselectedColor);

            CurrentTab = tab;

            CurrentTab.Show();
            CurrentTab.SetButtonColor(_selectedColor);

            _scrollRect.content = CurrentTab.RectTab;

            OnSelected.Invoke(CurrentTab);
        }
    }
}