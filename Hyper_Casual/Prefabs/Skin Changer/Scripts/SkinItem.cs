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

        public void ShowAsUnlock()
        {
            _lockedIcon.DisactiveSelf();
        }

        public void ShowAsSelected()
        {
            _lockedIcon.DisactiveSelf();
            _selectedIcon.ActiveSelf();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked.Invoke(this);
        }
    }
}