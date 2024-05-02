using UnityEngine;

public class WeaponsBuying : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayersWeapon _playersWeapon;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _playersWeapon.Weapons.Count; i++)
        {
            AddItem(_playersWeapon.Weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(weapon);
        view.GetLinkPlayer(_playersWeapon);
    }

    private void OnSellButtonClick(Weapon weapon,WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }

    private void TrySellWeapon(Weapon weapon,WeaponView view)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            weapon.WeaponState.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}
