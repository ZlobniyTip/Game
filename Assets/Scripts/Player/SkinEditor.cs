using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    [SerializeField] private GameObject _currentSkin;

    public void ChangeSkin(GameObject skin)
    {
        _currentSkin = skin;
        skin.SetActive(true);
        _currentSkin.SetActive(false);
    }
}