using System.Collections.Generic;
using UnityEngine;

public class PlayersWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Thrower _thrower;

    private int _indexCurrentWeapon = 0;

    public List<Weapon> Weapons => _weapons;
    public Weapon CurrentWepon => _currentWeapon;

    private void Awake()
    {
        _thrower.GetWeapon(_currentWeapon);
    }

    private void Start()
    {
        _indexCurrentWeapon = PlayerPrefs.GetInt("IndexCurrentWeapon");
        ChangeWeapon(_weapons[_indexCurrentWeapon]);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon.WeaponState.Used(false);
        _currentWeapon = weapon;
        _thrower.GetWeapon(_currentWeapon);
        _indexCurrentWeapon = weapon.Index;
        PlayerPrefs.SetInt("IndexCurrentWeapon", _indexCurrentWeapon);
    }

    public void LoadWeapons(List<WeaponState> weapons)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].WeaponState.LoadState(weapons[i].IsBuying, weapons[i].IsUsed);
        }
    }
}