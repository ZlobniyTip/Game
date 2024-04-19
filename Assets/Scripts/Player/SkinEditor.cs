using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Skin _currentSkin;
    [SerializeField] private Player _player;

    private int _indexCurrentSkin = 0;

    public List<Skin> Skins => _skins;

    private void Start()
    {
        _indexCurrentSkin = PlayerPrefs.GetInt("IndexCurrentSkin");

        ChangeSkin(_skins[_indexCurrentSkin]);
    }

    public void ChangeSkin(Skin skin)
    {
        _currentSkin.gameObject.SetActive(false);
        _currentSkin = skin;
        _currentSkin.gameObject.SetActive(true);
        _indexCurrentSkin = skin.Index;
        PlayerPrefs.SetInt("IndexCurrentSkin", _indexCurrentSkin);
    }
}