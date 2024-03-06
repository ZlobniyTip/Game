using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _speedRotation;

    private Player _player;
    private Ricochet _ricochet;
    private Rigidbody _rigidbody;
    private Ray _directionRay;
    private RaycastHit _hitPoint;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitRagdollBone bone))
        {
            bone.TakeHit(transform.forward);
            _player.GetReward(bone.Reward);
        }

        if (collision.gameObject.TryGetComponent(out ExplosiveBarrel explosiveBarrel))
        {
            explosiveBarrel.Explode();
        }

        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            _ricochet.CalculateRicochet(_rigidbody, _directionRay, _hitPoint);
        }
    }

    public void Die()
    {
        StopCoroutine(Spin());
        Destroy(gameObject);
    }

    public IEnumerator Spin()
    {
        float timer = 0;
        float torsionTime = 2.5f;

        while (timer < torsionTime)
        {
            timer += Time.deltaTime;

            if (this != null)
            {
                transform.Rotate(0, 0, _speedRotation * Time.deltaTime);
            }

            yield return null;
        }
    }

    public void GetLinks(Player player, Ricochet ricochet)
    {
        _player = player;
        _ricochet = ricochet;
    }
}