using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
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

            UpdateInformation(_tabSelecter.CurrentTab);
        }

        private void OnDisable()
        {
            _tabSelecter.OnSelected -= OnUpdateInformation;
        }

        private void OnUpdateInformation(Tab tab)
        {
            UpdateInformation(tab);
        }

        private void UpdateInformation(Tab tab)
        {
            UnlockBoughts(tab);

            SkinItem wearSkin = GetWearSkin(tab);
            wearSkin.ShowAsSelected();
        }

        private void UnlockBoughts(Tab tab)
        {
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == tab.GroupId);
            Assert.IsTrue(skinSet == default, $"No found SkinSet with id: {tab.GroupId}");

            foreach (int idBought in skinSet.BoughtIds)
            {
                SkinItem skinItem = tab.SkinItems.FirstOrDefault(skinItem => skinItem.Data.Id == idBought);
                if (skinItem == default)
                    continue;

                skinItem.ShowAsUnlock();
            }
        }

        public SkinItem GetWearSkin(Tab tab)
        {
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == tab.GroupId);
            Assert.IsTrue(skinSet == default, $"No found SkinSet with id: {tab.GroupId}");

            int id = skinSet.IsEquipped == true ? skinSet.EquippedId : skinSet.BoughtIds[0];
            SkinItem skinItem = tab.SkinItems.FirstOrDefault(skinItem => skinItem.Data.Id == id);
            Assert.IsTrue(skinItem == default, $"No found SkinItem with id: {id}");

            return skinItem;
        }
    }
}