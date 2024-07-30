using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class UnitRagdollBone : MonoBehaviour
{
    [SerializeField] private int _reward;

    private Rigidbody _rigidbody;

    public int Reward => _reward;

    public UnityAction<UnitRagdollBone, Vector3> GetHit;

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