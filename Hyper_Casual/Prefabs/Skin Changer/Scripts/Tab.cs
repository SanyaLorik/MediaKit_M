using SanyaBeerExtension;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    [Serializable]
    public class Tab
    {
        [SerializeField] private RectTransform _tab;
        [SerializeField] private Button _button;

        public event Action<Tab> OnClick = delegate { };

        public RectTransform RectTab => _tab;

        public void Initialize()
        {
            _button.onClick.AddListener(OnInvokeClick);
        }

        public void Deinitialize()
        {
            _button.onClick.RemoveListener(OnInvokeClick);
        }

        public void Show()
        {
            _tab.ActiveSelf();
        }

        public void Hide()
        {
            _tab.DisactiveSelf();
        }

        private void OnInvokeClick()
        {
            OnClick.Invoke(this);
        }
    }
}