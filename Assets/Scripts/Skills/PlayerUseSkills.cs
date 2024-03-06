using UnityEngine;

public class PlayerUseSkills : MonoBehaviour
{
    [SerializeField] private Thrower _thrower;

    public void UseAbillity(Skill skill)
    {
        skill.UseSkill(_thrower.CurrentWeapon);
    }
}