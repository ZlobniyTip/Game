using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Thrower))]
public class Teleportation : MonoBehaviour
{
    [SerializeField] private UnityEvent _teleportEffect;
    [SerializeField] private Offset _offset;
    [SerializeField] private ParticleSystem _prefab;

    public event UnityAction UseTeleportation;

    private Rigidbody _rigidbody;
    private Weapon _currentWeapon;
    private Thrower _thrower;
    private bool _isCursorOnPlayer;
    private ParticleSystem _smoke;

    private void Start()
    {
        _thrower = GetComponent<Thrower>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseEnter()
    {
        _isCursorOnPlayer = true;
    }

    private void OnMouseExit()
    {
        _isCursorOnPlayer = false;
    }

    private void Update()
    {
        if (transform.position.z != 0)
        {
            transform.position = transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (_isCursorOnPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _teleportEffect?.Invoke();
                _smoke = Instantiate(_prefab, transform.position, Quaternion.Euler(180, 0, 0));
                _smoke.Play();
            }
        }

        if (_thrower.WeaponsFlight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UseTeleportation?.Invoke();
                _rigidbody.isKinematic = false;
                _offset.ActivateOffset(true);
                TeleportWeapon();
                _offset.CheckCollision();
                _teleportEffect?.Invoke();
                _smoke = Instantiate(_prefab, transform.position, Quaternion.Euler(180, 0, 0));
                _smoke.Play();
                _thrower.ResetFlightStatusWeapon();
            }
        }
    }

    public void GetLinkCurrentWeapon(Weapon currentWeapon)
    {
        _currentWeapon = currentWeapon;
    }

    private void TeleportWeapon()
    {
        if (_currentWeapon != null)
        {
            transform.position = _currentWeapon.transform.position;
            _currentWeapon.Die();
            _currentWeapon = null;
        }
    }
}