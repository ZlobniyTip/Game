using Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace User
{
    public class PlayerSkills : ProductList
    {
        [SerializeField] private PlayerUseSkills _playerUseSkills;

        private Skill _currentSkill;

        public void EquipSkill(Product product)
        {
            if (_currentSkill != null)
            {
                _currentSkill.State.SetStatus(ItemStatus.Purchased);
            }

            product.State.SetStatus(ItemStatus.Equipped);

            _currentSkill = (Skill)product;
            _playerUseSkills.EquipSkill(_currentSkill);
        }

        public void InitSkills(List<ItemStatus> skills)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].Init(skills[i]);

                if (Products[i].State.Status == ItemStatus.Equipped)
                {
                    EquipSkill((Skill)Products[i]);
                    _playerUseSkills.EquipSkill((Skill)Products[i]);
                }
            }
        }
    }
}