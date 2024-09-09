using UnityEngine;
using UnityEngine.Events;

public class ExplosionWeapon : Skill
{
    [SerializeField] private UnityEvent _explosionSound;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;

    public override void UseSkill(Weapon weapon)
    {
        if (this.State.Status == ItemStatus.Equipped)
        {
            Explosion(weapon);
        }
    }

    private void Explosion(Weapon weapon)
    {
        _explosionSound?.Invoke();
        Instantiate(_explosionEffect, weapon.transform).Play();

        weapon.Explosion(_radius, _force);
    }
}