using UnityEngine;

public class SkinBuying : MonoBehaviour
{
    [SerializeField] private SkinEditor _skinEditor;
    [SerializeField] private Player _player;
    [SerializeField] private SkinView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _skinEditor.Skins.Count; i++)
        {
            AddItem(_skinEditor.Skins[i]);
        }
    }

    private void AddItem(Skin skin)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(skin);
        view.GetLinkPlayer(_skinEditor);
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
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}