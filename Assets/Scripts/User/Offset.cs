using System.Linq;
using UnityEngine;

namespace User
{
    [RequireComponent(typeof(SphereCollider))]
    public class Offset : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private SphereCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
        }

        public void ActivateOffset(bool isActive)
        {
            _collider.isTrigger = !isActive;
        }

        public void CheckCollision()
        {
            int iterator = 0;

            while (true)
            {
                iterator++;
                Collider[] colliders = Physics.OverlapSphere(transform.position, _collider.radius);
                Collider[] filteredColliders = colliders.Where(c => c.transform.tag == "Collision").ToArray();

                if (filteredColliders.Length == 0)
                {
                    break;
                }

                if (iterator > 20)
                {
                    break;
                }

                Vector3 closestPoint = filteredColliders[0].ClosestPoint(transform.position);
                Vector3 direction = (transform.position - closestPoint).normalized;
                transform.position = closestPoint + _collider.radius * 1.1f * direction;
            }
        }
    }
}