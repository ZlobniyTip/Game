using UnityEngine;

public class Ricochet : Skill
{
    private Weapon _currentWeapon;
    private float _force = 500;
    private Vector3 _hitPoint;
    private Rigidbody _rigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            foreach (ContactPoint contactPoint in collision.contacts)
            {
                _hitPoint = contactPoint.normal;
                UseSkill(_currentWeapon);
            }
        }
    }

    public override void UseSkill(Weapon weapon)
    {
        if (this.SkillState.IsBuying)
        {
            UseRicochet();

            LockSkill();
        }
    }

    public void GetLinkCurrentWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void UseRicochet()
    {
        Debug.Log(_currentWeapon);
        _rigidbody = _currentWeapon.GetComponent<Rigidbody>();
        Debug.Log(_rigidbody);
        _rigidbody.AddForce(_hitPoint * _force);
    }
}