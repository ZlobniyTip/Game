using UnityEngine;

namespace User
{
    public class ThrowerRaycast : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;

        private Weapon _currentWeapon;
        private Ray _ray;
        private RaycastHit _hit;

        private void Update()
        {
            _ray = new (transform.position, transform.forward);

            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.black);

            if (Physics.Raycast(_ray, out _hit, _maxDistance))
            {
                _currentWeapon.transform.position = _hit.point;
            }
        }

        public void GetLinkWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
        }
    }
}