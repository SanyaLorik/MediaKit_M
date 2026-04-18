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