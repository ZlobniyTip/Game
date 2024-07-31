using UnityEngine;

public class Ricochet : Skill
{
    private Weapon _currentWeapon;
    private float _force = 500;
    private Rigidbody _rigidbody;
    private Vector3 _hitPoint;

    public void GetLinkCurrentWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _currentWeapon.Ricochet += UseRicochet;
    }

    private void UseRicochet(Vector3 hitPoint)
    {
        if (this.SkillState.IsBuying)
        {
            Debug.Log(_currentWeapon);
            _rigidbody = _currentWeapon.GetComponent<Rigidbody>();
            Debug.Log(_rigidbody);
            _rigidbody.AddForce(_hitPoint * _force);

            _currentWeapon.Ricochet -= UseRicochet;

            LockSkill();
        }
    }
}