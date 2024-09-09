using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshCollider))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private MeshCollider _meshCollider;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private int _index;

    [NonSerialized] private WeaponState _state = null;

    public event UnityAction Destruction;
    public event UnityAction<Vector3> Ricochet;

    private Vector3 _hitPoint;

    public string Label => _label;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public ItemState State => _state ??= new WeaponState(ItemStatus.NotPurchased);

    public Player Player { get; private set; }

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitRagdollBone bone))
        {
            bone.TakeHit(transform.forward);
            Player.GetReward(bone.Reward);
        }

        if (collision.gameObject.TryGetComponent(out ExplosiveBarrel explosiveBarrel))
        {
            explosiveBarrel.Explode();
        }

        if (collision.collider.gameObject.CompareTag("Collision"))
        {
            foreach (ContactPoint contactPoint in collision.contacts)
            {
                _hitPoint = contactPoint.normal;
                Ricochet?.Invoke(_hitPoint);
            }
        }
    }

    public void Init(ItemState weaponState)
    {
        if (weaponState == null)
            return;

        State.SetStatus(weaponState.Status);
    }

    public void Die()
    {
        Destroy(gameObject);
        Destruction?.Invoke();
    }

    public void GetLinks(Player player)
    {
        Player = player;
    }

    public void SwitchingCollider(bool isIncluded)
    {
        _meshCollider.enabled = isIncluded;
    }

    public void Explosion(float radius, float force)
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);
        Rigidbody rigidbody;

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            rigidbody = overlappedColliders[i].attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);

                if (rigidbody.gameObject.TryGetComponent(out UnitRagdollBone bone))
                {
                    Vector3 direction = bone.transform.position - transform.position;
                    bone.TakeHit(direction);
                }
            }
        }
    }
}