using SanyaBeerExtension;
using UnityEngine;

namespace MediaKit_M.SkinChanger
{
    public class SkinLocationActivity : MonoBehaviour
    {
        [SerializeField] private GameObject _location;

        public void Show()
        {
            _location.ActiveSelf();
        }

        public void Hide()
        {
            _location.DisactiveSelf();
        }
    }
}