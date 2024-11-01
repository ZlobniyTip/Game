using UnityEngine;

namespace User
{
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
                transform.LookAt(_thrower.CurrentTarget.transform);
                return;
            }

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}