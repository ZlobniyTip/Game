using UnityEngine;
using UnityEngine.Events;

public class ExplosionWeapon : Skill
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private UnityEvent _explosionSound;
    [SerializeField] private ParticleSystem _explosionEffect;

    public override void UseSkill(Weapon weapon)
    {
        if (this.SkillState.IsBuying)
        {
            Explosion(weapon);
        }
    }

    private void Explosion(Weapon weapon)
    {
        _explosionSound?.Invoke();
        _explosionEffect.transform.position = weapon.transform.position;
        _explosionEffect.Play();

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
        Rigidbody rigidbody;

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            rigidbody = overlappedColliders[i].attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddExplosionForce(_force, weapon.transform.position, _radius);

                if (rigidbody.gameObject.TryGetComponent(out UnitRagdollBone bone))
                {
                    Vector3 direction = bone.transform.position - transform.position;
                    bone.TakeHit(direction);
                }
            }
        }
    }
}