using System.Collections.Generic;
using UnityEngine;

public class PlayersWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Thrower _thrower;

    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;

    public List<Weapon> Weapons => _weapons;

    private void Start()
    {
        _currentWeaponIndex = PlayerPrefs.GetInt("IndexCurrentWeapon", 0);

        EquipWeapon(_weapons[_currentWeaponIndex]);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.State.SetStatus(ItemStatus.Purchased);

        weapon.State.SetStatus(ItemStatus.Equipped);

        _currentWeapon = weapon;
        _currentWeaponIndex = weapon.Index;
        _thrower.SetWeapon(_currentWeapon);

        PlayerPrefs.SetInt("IndexCurrentWeapon", _currentWeaponIndex);
    }

    public void InitWeapons(List<ItemStatus> weapons)
    {
        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].Init(weapons[i]);
    }

    public void OverrideCurrentWeapon(Weapon weapon)
    {
        EquipWeapon(weapon);
    }
}