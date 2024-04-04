using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Thrower))]
public class Teleportation : MonoBehaviour
{
    [SerializeField] private UnityEvent _teleportEffect;

    private Weapon _currentWeapon;
    private Thrower _thrower;

    private void Start()
    {
        _thrower = GetComponent<Thrower>();
    }

    private void Update()
    {
        if (_thrower.AimingMode == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TeleportWeapon();
                _teleportEffect?.Invoke();
            }
        }
    }

    public void GetLinkCurrentWeapon(Weapon currentWeapon)
    {
        _currentWeapon = currentWeapon; 
        _teleportEffect?.Invoke();
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