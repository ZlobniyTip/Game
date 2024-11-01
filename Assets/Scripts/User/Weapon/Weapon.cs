using Enemy;
using Environment;
using Save;
using UnityEngine;
using UnityEngine.Events;

namespace User
{
    [RequireComponent(typeof(MeshCollider))]
    public class Weapon : Product
    {
        [SerializeField] private MeshCollider _meshCollider;

        private Vector3 _hitPoint;

        public event UnityAction Destructed;
        public event UnityAction<Vector3> RicochetHappened;

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
                    RicochetHappened?.Invoke(_hitPoint);
                }
            }
        }

        public override void Init(ItemStatus state)
        {
            State.SetStatus(state);
        }

        public void Die()
        {
            Destroy(gameObject);
            Destructed?.Invoke();
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
}