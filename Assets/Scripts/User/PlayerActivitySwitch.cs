using UnityEngine;

namespace User
{
    public class PlayerActivitySwitch : MonoBehaviour
    {
        [SerializeField] private AudioSource _effect;
        [SerializeField] private RaycastIgnoreLayer _raycastIgnore;

        public void Switching(bool isActive)
        {
            switch (isActive)
            {
                case true:
                    _raycastIgnore.enabled = false;
                    _effect.gameObject.SetActive(false);
                    break;

                case false:
                    _raycastIgnore.enabled = true;
                    _effect.gameObject.SetActive(true);
                    break;
            }
        }
    }
}