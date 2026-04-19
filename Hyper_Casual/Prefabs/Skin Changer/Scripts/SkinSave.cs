using System;
using System.Collections.Generic;

namespace MediaKit_M.SkinChanger
{
    [Serializable]
    public class SkinSave
    {
        public List<SkinSet> SkinSets = new()
        {
            new() { GroupId = 1 },
            new() { GroupId = 2 },
            new() { GroupId = 3 },
            new() { GroupId = 4 },
            new() { GroupId = 5 }
        };

        public event Action<List<SkinSet>> OnSetsUpdated;
        public event Action<IReadOnlyList<KeyValuePair<int, SkinData>>> OnWearUpdated;

        public IReadOnlyList<KeyValuePair<int, SkinData>> WearSkins { get; private set; }

        public void NotifyAboutUpdateSets()
        {
            OnSetsUpdated?.Invoke(SkinSets);
        }

        public void NotifyAboutUpdateWear(IReadOnlyList<KeyValuePair<int, SkinData>> keyValuePairs)
        {
            WearSkins = keyValuePairs;
            OnWearUpdated?.Invoke(WearSkins);
        }
    }

    [Serializable]
    public class SkinSet
    {
        public int GroupId = 1;
        public List<int> BoughtIds = new() { 1 };
        public int EquippedId = -1;

        public bool IsEquipped => EquippedId != -1;
    }
}