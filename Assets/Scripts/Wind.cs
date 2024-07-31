using UnityEngine;
using UnityEngine.Events;

public class Wind : MonoBehaviour
{
    [SerializeField] private LayerMask _weaponLayer;
    [SerializeField] Teleportation _player;
    [SerializeField] private float _radius;

    public event UnityAction<Vector3> ChangingWindDirection;
    public event UnityAction<float> ChangingWindForce;

    private Vector3 _wind;

    public Vector3 WindDirection { get; private set; }
    public float WindForce { get; private set; }

    private void Start()
    {
        ChangeWind();
    }

    private void OnEnable()
    {
        _player.UseTeleportation += ChangeWind;
    }

    private void OnDisable()
    {
        _player.UseTeleportation -= ChangeWind;
    }

    private void Update()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius, _weaponLayer);
        Rigidbody rigidbody;

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            rigidbody = overlappedColliders[i].attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddForce(_wind);
            }
        }
    }

    private void ChangeWind()
    {
        _wind = ChangeWindDirection() * ChangeWindForce();
    }

    private float ChangeWindForce()
    {
        WindForce = Random.Range(0.05f, 1f);

        ChangingWindForce?.Invoke(WindForce);

        ChangeWindDirection();

        return WindForce;
    }

    private Vector3 ChangeWindDirection()
    {
        if (WindForce < 0.5f)
        {
            WindDirection = Vector3.left;
        }

        if (WindForce >= 0.5f)
        {
            WindDirection = Vector3.right;
        }

        ChangingWindDirection?.Invoke(WindDirection);

        return WindDirection;
    }
}