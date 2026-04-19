using Architecture_M;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace MediaKit_M.SkinChanger
{
    public class SkinCollection
    {
        private TabSelecter _tabSelecter;
        private SkinSave _save;
        private IGameSave _gameSave;

        public SkinItem CurrentSkin { get; private set; }

        private Dictionary<int, SkinData> _equippedSkins;

        public void Initialize(TabSelecter tabSelecter, SkinSave save, IGameSave gameSave)
        {
            _tabSelecter = tabSelecter;
            _save = save;
            _gameSave = gameSave;

            _equippedSkins = new(tabSelecter.Tabs.Count);
        }

        public void SetupInitial()
        {
            foreach (Tab tab in _tabSelecter.Tabs)
                UpdateInformation(tab);

            UpdateEquippedWear();

            _save.NotifyAboutUpdateSets();
            _save.NotifyAboutUpdateWear(_equippedSkins.ToArray());
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

            CurrentSkin = GetWearSkin(tab);
            CurrentSkin.ShowAsSelected();
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

            skinSet.EquippedId = id;

            return skinItem;
        }

        public void SetCurrentWear(SkinItem currentSkin)
        {
            CurrentSkin?.ShowAsUnlock();
            CurrentSkin = currentSkin;
            CurrentSkin.ShowAsSelected();

            int currentTabId = _tabSelecter.CurrentTab.GroupId;
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == currentTabId);
            Assert.IsFalse(skinSet == default, $"No found SkinSet with id: {currentTabId}");

            skinSet.EquippedId = CurrentSkin.Data.Id;

            UpdateEquippedWear();

            _save.NotifyAboutUpdateSets();
            _save.NotifyAboutUpdateWear(_equippedSkins.ToArray());
        }

        public void AddWear(SkinItem currentSkin)
        {
            int currentTabId = _tabSelecter.CurrentTab.GroupId;
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == currentTabId);
            Assert.IsFalse(skinSet == default, $"No found SkinSet with id: {currentTabId}");

            skinSet.BoughtIds.Add(currentSkin.Data.Id);

            _gameSave.Save();
        }

        private void UpdateEquippedWear()
        {
            foreach (SkinSet skinSet in _save.SkinSets)
            {
                Tab tab = _tabSelecter.Tabs.FirstOrDefault(tab => tab.GroupId == skinSet.GroupId);
                Assert.IsFalse(tab == default, $"No found Tab with id: {skinSet.GroupId}");

                SkinItem skinItem = tab.SkinItems.FirstOrDefault(skin => skin.Data.Id == skinSet.EquippedId);
                Assert.IsFalse(tab == default, $"No found SkinItem with id: {skinSet.EquippedId}");

                if (_equippedSkins.ContainsKey(tab.GroupId) == true)
                    _equippedSkins[tab.GroupId] = skinItem.Data;
                else
                    _equippedSkins.Add(tab.GroupId, skinItem.Data);
            }
        }
    }
}