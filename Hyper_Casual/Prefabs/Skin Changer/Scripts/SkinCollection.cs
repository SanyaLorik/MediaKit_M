using UnityEngine;
using Zenject;

namespace MediaKit_M.SkinChanger
{
    public class SkinCollection : MonoBehaviour 
    {
        [SerializeField] private TabSelecter _tabSelecter;

        [Inject] private ISkinSaveLoader _loader;

        private SkinSave _save;

        private void Awake()
        {
            _save = _loader.Load();
        }

        private void OnEnable()
        {
            _tabSelecter.OnSelected += OnUpdateInformation;
        }

        private void OnDisable()
        {
            _tabSelecter.OnSelected -= OnUpdateInformation;
        }

        private void OnUpdateInformation(Tab tab)
        {

        }
    }
}