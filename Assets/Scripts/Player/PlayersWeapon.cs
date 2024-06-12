using System.Collections.Generic;
using UnityEngine;

public class PlayersWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Thrower _thrower;

    private int _indexCurrentWeapon = 0;

    public List<Weapon> Weapons => _weapons;

    private void Start()
    {
        _indexCurrentWeapon = PlayerPrefs.GetInt("IndexCurrentWeapon");
        ChangeWeapon(_weapons[_indexCurrentWeapon]);
        _thrower.GiveWeapon(_currentWeapon);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _thrower.GiveWeapon(_currentWeapon);
        _indexCurrentWeapon = weapon.Index;
        PlayerPrefs.SetInt("IndexCurrentWeapon", _indexCurrentWeapon);
    }

    public void LoadWeapons(List<WeaponState> weapons)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].WeaponState.LoadState(weapons[i].IsBuying);
        }
    }
}