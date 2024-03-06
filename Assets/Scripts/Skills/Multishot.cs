using UnityEngine;

public class Multishot : Skill
{
    public override void UseSkill(Weapon weapon)
    {
        Weapon firstweapon = Instantiate(weapon, weapon.transform);
        Weapon secondWeapon = Instantiate(weapon, weapon.transform);

        firstweapon.GetComponent<Rigidbody>().AddForce(new Vector3(firstweapon.transform.position.x, firstweapon.transform.position.y + 15));
        secondWeapon.GetComponent<Rigidbody>().AddForce(new Vector3(firstweapon.transform.position.x, firstweapon.transform.position.y + 15));
    }
}