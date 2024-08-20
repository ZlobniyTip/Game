using System.Collections.Generic;
using UnityEngine;

public class SkinBuying : MonoBehaviour
{
    [SerializeField] private SkinEditor _skinEditor;
    [SerializeField] private Player _player;
    [SerializeField] private SkinView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<SkinView> _content = new List<SkinView>();
    private SkinView _usedSkinView;

    private void OnEnable()
    {
        for (int i = 0; i < _skinEditor.Skins.Count; i++)
        {
            AddItem(_skinEditor.Skins[i]);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            _content[i].SellButtonClick -= OnSellButtonClick;
            _content[i].UsedSkinView -= GetLinkSkinView;
            _content[i].ChangeUsedSkin -= OnChangeUsedSkin;
            Destroy(_content[i].gameObject);
        }

        _content.Clear();
    }

    private void AddItem(Skin skin)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.UsedSkinView += GetLinkSkinView;
        view.ChangeUsedSkin += OnChangeUsedSkin;
        view.Render(skin);
        view.GetLinkPlayer(_skinEditor);
        _content.Add(view);
    }

    private void OnSellButtonClick(Skin skin, SkinView view)
    {
        TrySellSkin(skin, view);
    }

    private void TrySellSkin(Skin skin, SkinView view)
    {
        if (skin.Price <= _player.Money)
        {
            _player.BuySkin(skin);

            if (_usedSkinView != null)
            {
                _usedSkinView.LockItem();
            }

            view.ShowUsedButton();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

    public void GetLinkSkinView(SkinView skinView)
    {
        _usedSkinView = skinView;
    }

    private void OnChangeUsedSkin(SkinView skinView)
    {
        if (_usedSkinView != null)
        {
            _usedSkinView.LockItem();
        }

        _usedSkinView = skinView;
        skinView.ShowUsedButton();
    }
}