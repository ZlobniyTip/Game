using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _speedRotation;

    private Rigidbody _rigidbody;
    private Ray _directionRay;
    private RaycastHit _hitPoint;
    private Coroutine _spin;

    public Player Player { get; private set; }
    public Ricochet Ricochet { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_spin == null)
        {
            StartSpin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitRagdollBone bone))
        {
            StopSpin();
            bone.TakeHit(transform.forward);
            Player.GetReward(bone.Reward);
        }

        if (collision.gameObject.TryGetComponent(out ExplosiveBarrel explosiveBarrel))
        {
            StopSpin();
            explosiveBarrel.Explode();
        }

        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            StopSpin();
            //_ricochet.CalculateRicochet(_rigidbody, _directionRay, _hitPoint);
        }
    }

    public void Die()
    {
        StopSpin();
        Destroy(gameObject);
    }

    public void StartSpin()
    {
        _spin = StartCoroutine(Spin());
    }
    public void GetLinks(Player player, Ricochet ricochet)
    {
        Player = player;
        Ricochet = ricochet;
    }

    private IEnumerator Spin()
    {
        var torsionDuration = new WaitForSeconds(3);

        while (true)
        {
            transform.Rotate(_speedRotation * Time.deltaTime, 0, 0);

            yield return null;
        }
    }

    private void StopSpin()
    {
        if (_spin != null)
        {
            StopCoroutine(_spin);
        }
    }
}