using UnityEngine;

public class Ricochet : Skill
{
    [SerializeField] private Weapon _currentWeapon;

    private float _force = 500;
    private Rigidbody _rigidbody;

    public void GetLinkCurrentWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _currentWeapon.Ricochet += UseRicochet;
    }

    private void UseRicochet(Vector3 hitPoint)
    {
        if (this.SkillState.IsBuying)
        {
            _rigidbody = _currentWeapon.GetComponent<Rigidbody>();
            _rigidbody.AddForce(hitPoint * _force);

            _currentWeapon.Ricochet -= UseRicochet;

            LockSkill();
        }
    }
}