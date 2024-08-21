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
        if (PlayerPrefs.HasKey("IndexCurrentSkin"))
        {
            _indexCurrentSkin = PlayerPrefs.GetInt("IndexCurrentSkin");
            ChangeSkin(_skins[_indexCurrentSkin]);
        }
    }

    public void ChangeSkin(Skin skin)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            _skins[i].SkinState.Used(false);
        }

        skin.SkinState.Used(true);
        _currentSkin.gameObject.SetActive(false);
        _currentSkin = skin;
        _currentSkin.gameObject.SetActive(true);
        _indexCurrentSkin = skin.Index;
        PlayerPrefs.SetInt("IndexCurrentSkin", _indexCurrentSkin);
    }

    public void LoadSkins(List<SkinState> skins)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            _skins[i].SkinState.LoadState(skins[i].IsBuying, skins[i].IsUsed);
        }
    }
}