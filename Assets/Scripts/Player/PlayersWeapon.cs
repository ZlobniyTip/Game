using System.Collections.Generic;
using UnityEngine;

public class PlayersWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Thrower _thrower;

    private int _currentWeaponIndex = 0;
    private Weapon _currentWeapon;

    public List<Weapon> Weapons => _weapons;

    private void Start()
    {
        _currentWeaponIndex = PlayerPrefs.GetInt("IndexCurrentWeapon", 0);
        
        EquipWeapon(_weapons[_currentWeaponIndex]);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.State.SetStatus(WeaponStatus.Purchased);
        
        weapon.State.SetStatus(WeaponStatus.Equipped);
        
        _currentWeapon = weapon;
        _currentWeaponIndex = weapon.Index;
        _thrower.SetWeapon(_currentWeapon);
        
        PlayerPrefs.SetInt("IndexCurrentWeapon", _currentWeaponIndex);
    }

    public void InitWeapons(List<WeaponState> weapons)
    {
        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].Init(weapons[i]);
    }
}