using Save;
using UnityEngine;

namespace User
{
    public class Ricochet : Skill
    {
        private readonly float _force = 300;
        private Weapon _currentWeapon;
        private Rigidbody _rigidbody;

        public void GetLinkCurrentWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            _currentWeapon.RicochetHappened += UseRicochet;
        }

        private void UseRicochet(Vector3 hitPoint)
        {
            if (this.State.Status == ItemStatus.Equipped)
            {
                _rigidbody = _currentWeapon.GetComponent<Rigidbody>();
                _rigidbody.AddForce(hitPoint * _force);

                _currentWeapon.RicochetHappened -= UseRicochet;

                LockSkill();
            }
        }
    }
}