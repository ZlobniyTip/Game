using Save;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace User
{
    public class PlayersWeapon : ProductList
    {
        [SerializeField] private Thrower _thrower;

        private Weapon _currentWeapon;

        public void EquipWeapon(Product weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.State.SetStatus(ItemStatus.Purchased);
            }

            weapon.State.SetStatus(ItemStatus.Equipped);

            _currentWeapon = (Weapon)weapon;
            _thrower.SetWeapon(_currentWeapon);
        }

        public void InitWeapons(List<ItemStatus> weapons)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].Init(weapons[i]);

                if (Products[i].State.Status == ItemStatus.Equipped)
                {
                    EquipWeapon(Products[i]);
                }
            }
        }
    }
}