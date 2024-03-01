using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private UnityEvent _exlosionEffect;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;

    public void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
        Rigidbody rigidbody;

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            rigidbody = overlappedColliders[i].attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddExplosionForce(_force, transform.position, _radius);

                if (rigidbody.gameObject.TryGetComponent(out UnitRagdollBone bone))
                {
                    Vector3 direction = bone.transform.position - transform.position;
                    bone.TakeHit(direction);
                }
            }
        }

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        var delay = new WaitForSeconds(1);

        _exlosionEffect?.Invoke();

        yield return delay;

        Destroy(gameObject);
    }
}
