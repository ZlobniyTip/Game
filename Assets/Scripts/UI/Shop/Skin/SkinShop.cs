using System.Collections.Generic;
using UnityEngine;

public class SkinShop : MonoBehaviour
{
    [SerializeField] private SkinEditor _skinEditor;
    [SerializeField] private Player _player;
    [SerializeField] private SkinView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<SkinView> _content = new List<SkinView>();

    private void OnEnable()
    {
        foreach (var skin in _skinEditor.Skins)
            AddSkinView(skin);
    }

    private void OnDisable()
    {
        foreach (var skinView in _content)
        {
            skinView.PurchaseButtonPressed -= OnPurchaseButtonPressed;
            skinView.EquipButtonPressed -= OnEquipButtonPressed;
            Destroy(skinView.gameObject);
        }

        _content.Clear();
    }

    private void AddSkinView(Skin skin)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(skin);
        view.PurchaseButtonPressed += OnPurchaseButtonPressed;
        view.EquipButtonPressed += OnEquipButtonPressed;
        _content.Add(view);
    }

    private void OnPurchaseButtonPressed(SkinView view)
    {
        _player.TryPurchaseSkin(view.Skin);
    }

    private void OnEquipButtonPressed(SkinView view)
    {
        _player.EquipSkin(view.Skin);
    }
}