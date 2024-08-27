using UnityEngine;

public class SwitchingWorkWithUI : MonoBehaviour
{
    [SerializeField] private AudioSource _effect;
    [SerializeField] private RaycastIgnoreLayer _raycastIgnore;

    public void Switching()
    {
        _raycastIgnore.enabled = !_raycastIgnore.enabled;

        if (_effect.gameObject.activeSelf)
        {
            _effect.gameObject.SetActive(false);
        }
        else
        {
            _effect.gameObject.SetActive(true);
        }
    }
}
