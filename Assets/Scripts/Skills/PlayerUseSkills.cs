using UnityEngine;

public class PlayerUseSkills : MonoBehaviour
{
    private Skill _usedSkill;
    private Weapon _currentWeapon;

    private void Update()
    {
        if (_usedSkill != null)
        {
            UseAbillity(_usedSkill);
            _usedSkill = null;
        }
    }

    public void GetSkill(Skill skill)
    {
        _usedSkill = skill;
    }

    public void GetLinkWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void UseAbillity(Skill skill)
    {
        skill.UseSkill(_currentWeapon);
    }
}