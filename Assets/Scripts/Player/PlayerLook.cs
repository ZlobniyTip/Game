using UnityEngine;

[RequireComponent(typeof(Thrower))]
public class PlayerLook : MonoBehaviour
{
    private Thrower _thrower;

    private void Start()
    {
        _thrower = GetComponent<Thrower>();
    }

    private void Update()
    {
        if (_thrower.AimingMode)
        {
            transform.LookAt(_thrower.CurrentWeapon.transform);
        }

        if (!_thrower.AimingMode)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}