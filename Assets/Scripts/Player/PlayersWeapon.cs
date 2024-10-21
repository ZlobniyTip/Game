using System.Collections.Generic;
using UnityEngine;

public class PlayersWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Thrower _thrower;

    private Weapon _currentWeapon;

    public List<Weapon> Weapons => _weapons;

    public void EquipWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.State.SetStatus(ItemStatus.Purchased);
        }

        weapon.State.SetStatus(ItemStatus.Equipped);

        _currentWeapon = weapon;
        _thrower.SetWeapon(_currentWeapon);
    }

    public void InitWeapons(List<ItemStatus> weapons)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].Init(weapons[i]);

            if (_weapons[i].State.Status == ItemStatus.Equipped)
            {
                EquipWeapon(_weapons[i]);
            }
        }
    }
}