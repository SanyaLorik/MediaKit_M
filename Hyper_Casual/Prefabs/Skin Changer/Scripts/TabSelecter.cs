using SanyaBeerExtension;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    public class TabSelecter : MonoBehaviour
    {
        [Header("Skins")]
        [SerializeField] private Tab[] _tabs;

        [Header("Content")]
        [SerializeField] private ScrollRect _scrollRect;

        private Tab _currentTab;

        private void Start()
        {
            _currentTab = _tabs[0];
        }

        private void OnEnable()
        {
            _tabs.ForEachs(tab => tab.Initialize(), tab => tab.OnClick += OnShowTab);
        }

        private void OnDisable()
        {
            _tabs.ForEachs(tab => tab.Deinitialize(), tab => tab.OnClick -= OnShowTab);
        }

        public void OnShowTab(Tab tab)
        {
            _currentTab.Hide();
            _currentTab = tab;
            _currentTab.Show();

            _scrollRect.content = _currentTab.RectTab;
        }
    }
}