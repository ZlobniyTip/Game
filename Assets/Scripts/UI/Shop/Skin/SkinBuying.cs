using System.Collections.Generic;
using UnityEngine;

public class SkinBuying : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Player _player;
    [SerializeField] private SkinView _template;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private SkinEditor _skinEditor;

    private void Start()
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            AddItem(_skins[i]);
        }
    }

    private void AddItem(Skin skin)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(skin);
    }

    private void OnSellButtonClick(Skin skin, SkinView view)
    {
        TrySellSkin(skin, view);
    }

    private void TrySellSkin(Skin skin, SkinView view)
    {
        if (skin.Price <= _player.Money)
        {
            _skinEditor.ChangeSkin(skin.gameObject);
            skin.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}