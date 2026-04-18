using Architecture_M;
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

        [Header("Animation")]
        [SerializeField] private DOTweenAnimationBase _selectedAnimation;

        public event Action<SkinItem> OnClicked = delegate { };

        public bool IsSelected { get; private set; } = false;

        public bool IsUnlocked { get; private set; } = false;

        public void ShowAsUnlock()
        {
            _lockedIcon.DisactiveSelf();
            _selectedIcon.DisactiveSelf();

            IsUnlocked = true;
            IsSelected = false;
        }

        public void ShowAsSelected()
        {
            _lockedIcon.DisactiveSelf();
            _selectedIcon.ActiveSelf();

            IsUnlocked = true;
            IsSelected = true;
        }

        public void StartSelectAnimation()
        {
            _selectedAnimation.Animate();
        }

        public void StopSelectAnimation()
        {
            _selectedAnimation.Kill();
            _selectedAnimation.ResetToInitialState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked.Invoke(this);
        }
    }
}