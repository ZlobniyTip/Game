using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponsBuying : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayersWeapon _playersWeapon;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<WeaponView> _content = new List<WeaponView>();
    private WeaponView _usedWeaponView;

    private void OnEnable()
    {
        for (int i = 0; i < _playersWeapon.Weapons.Count; i++)
        {
            AddItem(_playersWeapon.Weapons[i]);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            _content[i].SellButtonClick -= OnSellButtonClick;
            _content[i].UsedWeaponView -= GetLinkWeaponView;
            _content[i].ChangeUsedWeapon -= OnChangeUsedWeapon;
            Destroy(_content[i].gameObject);
        }

        _content.Clear();
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.UsedWeaponView += GetLinkWeaponView;
        view.ChangeUsedWeapon += OnChangeUsedWeapon;
        view.Render(weapon);
        view.GetLinkPlayer(_playersWeapon);
        _content.Add(view);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);

            if (_usedWeaponView != null)
            {
                _usedWeaponView.LockItem();
            }

            _usedWeaponView = view;
            view.ShowUsedButton();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

    public void GetLinkWeaponView(WeaponView weaponView)
    {
        _usedWeaponView = weaponView;
    }

    private void OnChangeUsedWeapon(WeaponView weaponView)
    {
        if (_usedWeaponView != null)
        {
            _usedWeaponView.LockItem();
        }

        _usedWeaponView = weaponView;
        weaponView.ShowUsedButton();
    }
}
