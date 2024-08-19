using UnityEngine;
using UnityEngine.Events;

public class UseWeaponButton : MonoBehaviour
{
    [SerializeField] private WeaponView _weaponView;

    public event UnityAction ChangeWeapon;

    public void UseWeapon()
    {
        _weaponView.PlayersWeapon.ChangeWeapon(_weaponView.Weapon);
        _weaponView.ShowUsedButton();
        ChangeWeapon?.Invoke();
    }
}