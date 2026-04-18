using SanyaBeerExtension;
using System;
using System.Collections.Generic;
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

        public IReadOnlyCollection<Tab> Tabs => _tabs;

        public Tab CurrentTab { get; private set; }

        private void Awake()
        {
            CurrentTab = _tabs[0];
            ShowTab(CurrentTab);
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
            CurrentTab.Hide();
            CurrentTab = tab;
            CurrentTab.Show();

            _scrollRect.content = CurrentTab.RectTab;

            OnSelected.Invoke(CurrentTab);
        }
    }
}