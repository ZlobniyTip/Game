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
        _currentSkinIndex = PlayerPrefs.GetInt("IndexCurrentSkin");
        EquipSkin(_skins[_currentSkinIndex]);
    }

    public void EquipSkin(Skin skin)
    {
        if (_currentSkin != null)
            _currentSkin.State.SetStatus(SkinStatus.Purchased);

        skin.State.SetStatus(SkinStatus.Equipped);

        _currentSkin = skin;
        _currentSkinIndex = skin.Index;

        PlayerPrefs.SetInt("IndexCurrentWeapon", _currentSkinIndex);
    }

    public void InitSkins(List<SkinState> skins)
    {
        for (int i = 0; i < _skins.Count; i++)
            _skins[i].Init(skins[i]);
    }
}