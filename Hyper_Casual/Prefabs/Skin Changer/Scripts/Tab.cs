using SanyaBeerExtension;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MediaKit_M.SkinChanger
{
    [Serializable]
    public class Tab
    {
        [Header("View")]
        [SerializeField] private RectTransform _tab;
        [SerializeField] private Button _button;

        [Header("Skins")]
        [SerializeField] private SkinItem[] _skinItems;

        public event Action<Tab> OnClick = delegate { };

        public RectTransform RectTab => _tab;

        public IReadOnlyCollection<SkinItem> SkinItems => _skinItems;

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