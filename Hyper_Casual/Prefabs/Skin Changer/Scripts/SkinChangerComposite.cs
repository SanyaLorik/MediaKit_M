using UnityEngine;
using Zenject;

namespace MediaKit_M.SkinChanger
{
    public class SkinChangerComposite : MonoBehaviour 
    {
        [SerializeField] private TabSelecter _tabSelecter;
        [SerializeField] private SkinApplyer _skinApplyer;

        private SkinCollection _skinCollection = new();

        [Inject] private ISkinSaveLoader _skinSaveLoader;

        private void Awake()
        {
            SkinSave skinSave = _skinSaveLoader.Load();

            _skinCollection.Initialize(_tabSelecter, skinSave);
            _skinApplyer.Initialize(_tabSelecter, _skinCollection);
        }

        private void Start()
        {
            _tabSelecter.SetupInitial();
            _skinCollection.SetupInitial();
            _skinApplyer.SetupInitial();
        }

        private void OnEnable()
        {
            _tabSelecter.Enable();
            _skinCollection.Enable();
            _skinApplyer.Enable();
        }

        private void OnDisable()
        {
            _tabSelecter.Disable();
            _skinCollection.Disable();
            _skinApplyer.Disable();
        }
    }
}