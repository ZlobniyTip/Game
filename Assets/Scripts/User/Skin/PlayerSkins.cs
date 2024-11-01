using Save;
using System.Collections.Generic;
using UnityEngine;

namespace User
{
    public class PlayerSkins : ProductList
    {
        [SerializeField] private Player _player;

        private Skin _currentSkin;

        public void EquipSkin(Product product)
        {
            if (_currentSkin != null)
            {
                _currentSkin.State.SetStatus(ItemStatus.Purchased);
                _currentSkin.gameObject.SetActive(false);
            }

            product.State.SetStatus(ItemStatus.Equipped);
            product.gameObject.SetActive(true);

            _currentSkin = (Skin)product;
        }

        public void InitSkins(List<ItemStatus> skins)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].Init(skins[i]);

                if (Products[i].State.Status == ItemStatus.Equipped)
                {
                    EquipSkin((Skin)Products[i]);
                }
            }
        }
    }
}