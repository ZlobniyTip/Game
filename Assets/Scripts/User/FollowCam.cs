using UnityEngine;

namespace User
{
    public class FollowCam : MonoBehaviour
    {
        [SerializeField] private float _easing;

        private Transform _pointOfInterest;
        private Vector3 _destination;
        private float _camCoordinate;

        private void Awake()
        {
            _camCoordinate = transform.position.z;
        }

        private void FixedUpdate()
        {
            if (_pointOfInterest == null)
                return;

            _destination = _pointOfInterest.transform.position;
            _destination = Vector3.Lerp(transform.position, _destination, _easing);
            _destination.z = _camCoordinate;
            transform.position = _destination;
        }

        public void GetLinkToObservedObject(Transform weapon)
        {
            _pointOfInterest = weapon;
        }
    }
}