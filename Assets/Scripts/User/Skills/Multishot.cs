using Save;
using System.Collections;
using UnityEngine;

namespace User
{
    public class Multishot : Skill
    {
        private readonly float _firstWeaponAngle = 30;
        private readonly float _secondWeaponAngle = -30;

        public override void UseSkill(Weapon weapon)
        {
            if (this.State.Status == ItemStatus.Equipped)
            {
                MultipleWeapon(weapon, _firstWeaponAngle);
                MultipleWeapon(weapon, _secondWeaponAngle);

                LockSkill();
            }
        }

        private void MultipleWeapon(Weapon weapon, float angle)
        {
            var weaponRigidbody = weapon.GetComponent<Rigidbody>();

            Weapon multipleWeapon = Instantiate(weapon, weapon.transform.position, weapon.transform.rotation);
            multipleWeapon.GetLinks(weapon.Player);
            Rigidbody multyWeaponRigidbody = multipleWeapon.GetComponent<Rigidbody>();
            multyWeaponRigidbody.velocity = Quaternion.Euler(0, 0, angle) * weaponRigidbody.velocity;

            StartCoroutine(Die(multipleWeapon));
        }

        private IEnumerator Die(Weapon weapon)
        {
            float delay = 3;
            yield return new WaitForSeconds(delay);

            Destroy(weapon);
        }
    }
}