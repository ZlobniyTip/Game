using UnityEngine;

public class UseWeaponButton : MonoBehaviour
{
    [SerializeField] private WeaponView _weaponView;

    public void UseWeapon()
    {
        _weaponView.PlayersWeapon.ChangeWeapon(_weaponView.Weapon);
    }
}