using UnityEngine;

namespace MediaKit_M.SkinChanger
{
    public abstract class PurchaseAdapter : MonoBehaviour
    {
        public abstract bool CanSpend(int money);

        public abstract void Spend(int money);
    }
}