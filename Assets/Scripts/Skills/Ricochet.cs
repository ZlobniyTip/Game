using UnityEngine;

[RequireComponent(typeof(Thrower))]
public class Ricochet : MonoBehaviour
{
    [SerializeField] private float _ricochetForce;

    private Thrower _thrower;

    private void Start()
    {
        _thrower = GetComponent<Thrower>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            
        }
    }
}
