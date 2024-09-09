using System.Collections.Generic;
using UnityEngine;

public class PlayerUseSkills : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Skill _usedSkill;
    private Weapon _currentWeapon;
    private int _currentSkillIndex;

    private void Start()
    {
        if (PlayerPrefs.HasKey("IndexCurrentSkill"))
        {
            _currentSkillIndex = PlayerPrefs.GetInt("IndexCurrentSkill");
            EquipSkill(_player.Skills[_currentSkillIndex]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_usedSkill != null)
            {
                UseAbillity(_usedSkill);
                _usedSkill = null;
            }
        }
    }

    public void EquipSkill(Skill skill)
    {
        if (_usedSkill != null)
            _usedSkill.State.SetStatus(ItemStatus.Purchased);

        skill.State.SetStatus(ItemStatus.Equipped);

        _usedSkill = skill;
        _currentSkillIndex = skill.Index;

        PlayerPrefs.SetInt("IndexCurrentSkill", _currentSkillIndex);
    }

    public void GetLinkWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void OverrideCurrentSkill(Skill skill)
    {
        EquipSkill(skill);
    }

    private void UseAbillity(Skill skill)
    {
        skill.UseSkill(_currentWeapon);
    }
}