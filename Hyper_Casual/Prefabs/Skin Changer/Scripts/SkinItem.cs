using SanyaBeerExtension;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MediaKit_M.SkinChanger
{
    public class SkinItem : MonoBehaviour, IPointerClickHandler
    {
        [Header("Bought View")]
        [SerializeField] private GameObject _lockedIcon;
        [SerializeField] private GameObject _selectedIcon;

        [field: Header("Data")]
        [field: SerializeField] public SkinData Data { get; private set; }

        public event Action<SkinItem> OnClicked = delegate { };

        public bool IsSelected { get; private set; } = false;

        public bool IsUnlocked { get; private set; } = false;

        public void ShowAsUnlock()
        {
            _lockedIcon.DisactiveSelf();
            IsUnlocked = true;
        }

        public void ShowAsSelected()
        {
            _lockedIcon.DisactiveSelf();
            _selectedIcon.ActiveSelf();

            IsUnlocked = true;
            IsSelected = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked.Invoke(this);
        }
    }
}