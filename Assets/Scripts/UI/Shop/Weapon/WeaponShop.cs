using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<WeaponView> _content = new List<WeaponView>();

    private void OnEnable()
    {
        foreach (var weapon in _player.PlayersWeapon.Weapons)
            AddWeaponView(weapon);
    }

    private void OnDisable()
    {
        foreach (var weaponView in _content)
        {
            weaponView.PurchaseButtonPressed -= OnPurchaseButtonPressed;
            weaponView.EquipButtonPressed -= OnEquipButtonPressed;
            Destroy(weaponView.gameObject);
        }

        _content.Clear();
    }

    private void AddWeaponView(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(weapon);
        view.PurchaseButtonPressed += OnPurchaseButtonPressed;
        view.EquipButtonPressed += OnEquipButtonPressed;
        _content.Add(view);
    }

    private void OnPurchaseButtonPressed(WeaponView view)
    {
        _player.TryPurchaseWeapon(view.Weapon);
    }

    private void OnEquipButtonPressed(WeaponView view)
    {
        _player.EquipWeapon(view.Weapon);
    }
}