using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Player _player;

    private Skin _currentSkin;

    public List<Skin> Skins => _skins;

    public void EquipSkin(Skin skin)
    {
        if (_currentSkin != null)
        {
            _currentSkin.State.SetStatus(ItemStatus.Purchased);
            _currentSkin.gameObject.SetActive(false);
        }

        skin.State.SetStatus(ItemStatus.Equipped);
        skin.gameObject.SetActive(true);

        _currentSkin = skin;
    }

    public void InitSkins(List<ItemStatus> skins)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            _skins[i].Init(skins[i]);

            if (_skins[i].State.Status == ItemStatus.Equipped)
            {
                EquipSkin(_skins[i]);
            }
        }
    }
}