using System;
using System.Linq;
using UnityEngine.Assertions;

namespace MediaKit_M.SkinChanger
{
    public class SkinCollection
    {
        private TabSelecter _tabSelecter;
        private SkinSave _save;

        public void Initialize(TabSelecter tabSelecter, SkinSave save)
        {
            _tabSelecter = tabSelecter;
            _save = save;
        }

        public void SetupInitial()
        {
            UpdateInformation(_tabSelecter.CurrentTab);
        }

        public void Enable()
        {
            _tabSelecter.OnSelected += OnUpdateInformation;
        }

        public void Disable()
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

            SkinItem _currentWearItem = GetWearSkin(tab);
            _currentWearItem.ShowAsSelected();
        }

        private void UnlockBoughts(Tab tab)
        {
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == tab.GroupId);
            Assert.IsFalse(skinSet == default, $"No found SkinSet with id: {tab.GroupId}");

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
            Assert.IsFalse(skinSet == default, $"No found SkinSet with id: {tab.GroupId}");

            int id = skinSet.IsEquipped == true ? skinSet.EquippedId : skinSet.BoughtIds[0];
            SkinItem skinItem = tab.SkinItems.FirstOrDefault(skinItem => skinItem.Data.Id == id);
            Assert.IsFalse(skinItem == default, $"No found SkinItem with id: {id}");

            return skinItem;
        }
    }
}