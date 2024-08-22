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

    public event UnityAction<WeaponView> ChangeUsedWeapon;

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
            _content[i].ChangeUsedWeapon -= OnChangeUsedWeapon;
            Destroy(_content[i].gameObject);
        }

        _content.Clear();
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Render(weapon);
        view.SellButtonClick += OnSellButtonClick;
        view.ChangeUsedWeapon += OnChangeUsedWeapon;
        view.GetLinkPlayer(_playersWeapon, this);
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
            ChangeUsedWeapon?.Invoke(view);

            _player.BuyWeapon(weapon);

            for (int i = 0; i < _content.Count; i++)
            {
                if (_content[i].Weapon.WeaponState.IsBuying)
                {
                    _content[i].LockItem();
                }
            }

            view.ShowUsedButton();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

    private void OnChangeUsedWeapon(WeaponView weaponView)
    {
        ChangeUsedWeapon?.Invoke(weaponView);

        weaponView.ShowUsedButton();
    }
}