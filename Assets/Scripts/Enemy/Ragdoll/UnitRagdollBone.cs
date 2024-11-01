using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class UnitRagdollBone : MonoBehaviour
    {
        [SerializeField] private int _reward;

        private Rigidbody _rigidbody;

        public UnityAction<UnitRagdollBone, Vector3> GetHit;

        public int Reward => _reward;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void TakeHit(Vector3 direction)
        {
            GetHit?.Invoke(this, direction);
        }

        public void ApplyForce(Vector3 direction)
        {
            _rigidbody.AddForce(direction);
        }
    }
}