using System.Collections;
using UnityEngine;

public class Multishot : Skill
{
    [SerializeField] private float _force;

    private float _firstWeaponAngle = 30;
    private float _secondWeaponAngle = -30;

    public override void UseSkill(Weapon weapon)
    {
        Use(weapon, _firstWeaponAngle);
        Use(weapon, _secondWeaponAngle);
    }

    private void Use(Weapon weapon, float angle)
    {
        Weapon multiplyWeapon = Instantiate(weapon, weapon.transform.position, weapon.transform.rotation);
        multiplyWeapon.GetLinks(weapon.Player, weapon.Ricochet);
        Rigidbody multyWeaponRb = multiplyWeapon.GetComponent<Rigidbody>();
        multiplyWeapon.transform.Rotate(weapon.GetComponent<Rigidbody>().velocity, angle);
        multyWeaponRb.AddForce(multiplyWeapon.transform.up * _force);

        StartCoroutine(Die(multiplyWeapon));
    }

    private IEnumerator Die(Weapon weapon)
    {
        var delay = new WaitForSeconds(3);

        yield return delay;

        Destroy(weapon);
    }
}