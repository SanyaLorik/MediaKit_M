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

        private SkinItem _currentSkin;

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

            _currentSkin = GetWearSkin(tab);
            _currentSkin.ShowAsSelected();

            _save.NotifyAboutUpdateSets();
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

        public void SetCurrentWear(SkinItem currentSkin)
        {
            _currentSkin?.ShowAsUnlock();
            _currentSkin = currentSkin;
            _currentSkin.ShowAsSelected();

            int currentTabId = _tabSelecter.CurrentTab.GroupId;
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == currentTabId);
            Assert.IsFalse(skinSet == default, $"No found SkinSet with id: {currentTabId}");

            skinSet.EquippedId = _currentSkin.Data.Id;
        }

        public void AddWear(SkinItem currentSkin)
        {
            int currentTabId = _tabSelecter.CurrentTab.GroupId;
            SkinSet skinSet = _save.SkinSets.FirstOrDefault(skinSet => skinSet.GroupId == currentTabId);
            Assert.IsFalse(skinSet == default, $"No found SkinSet with id: {currentTabId}");

            skinSet.BoughtIds.Add(currentSkin.Data.Id);

            _gameSave.Save();
        }

        // ÂÛÄÀÒÜ ÄÅÔÎËÒÍÎÅ ÇÍÀ×ÅÍÈÅ ÏĞÈ ÑÒÀĞÒÅ È İÂÅÍÒ ÄËß ÏÎËÓ×ÅÍÈß ÍÎÂÃÎ ÑÊÈÍÀ
        private void SetEquippedWear(Tab tab)
        {
            int currentTabId = _tabSelecter.CurrentTab.GroupId;
            if (_equippedSkins.ContainsKey(currentTabId) == true)
                _equippedSkins[currentTabId] = currentSkin.Data;
            else
                _equippedSkins.Add(currentTabId, currentSkin.Data);
        }
    }
}