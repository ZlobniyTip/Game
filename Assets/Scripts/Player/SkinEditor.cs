using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Player _player;

    private int _currentSkinIndex = 0;
    private Skin _currentSkin;

    public List<Skin> Skins => _skins;

    private void Start()
    {
        if (PlayerPrefs.HasKey("IndexCurrentSkin"))
        {
            _currentSkinIndex = PlayerPrefs.GetInt("IndexCurrentSkin");
            _currentSkin = _skins[_currentSkinIndex];
            EquipSkin(_skins[_currentSkinIndex]);
        }
        else
        {
            _currentSkin = _skins[0];
            EquipSkin(_skins[0]);
        }
    }

    public void EquipSkin(Skin skin)
    {
        if (_currentSkin != null)
            _currentSkin.State.SetStatus(ItemStatus.Purchased);
            _currentSkin.gameObject.SetActive(false);

        skin.State.SetStatus(ItemStatus.Equipped);
        skin.gameObject.SetActive(true);

        _currentSkin = skin;
        _currentSkinIndex = skin.Index;

        PlayerPrefs.SetInt("IndexCurrentSkin", _currentSkinIndex);
    }

    public void InitSkins(List<ItemState> skins)
    {
        for (int i = 0; i < _skins.Count; i++)
            _skins[i].Init(skins[i]);
    }

    public void OverrideCurrentSkin(Skin skin)
    {
        EquipSkin(skin);
    }
}