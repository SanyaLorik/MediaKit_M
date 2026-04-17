using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MediaKit_M.SkinChanger
{
    public interface ISkinSaveLoader
    {
        SkinSave Load();
    }

    [Serializable]
    public class SkinSave
    {
        public List<int> IdBoughtSkins;
        public List<int> IdWearSkins;
    }

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