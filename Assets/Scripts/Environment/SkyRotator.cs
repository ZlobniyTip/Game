using UnityEngine;

namespace Environment
{
    public class SkyRotator : MonoBehaviour
    {
        [SerializeField] private float _speedRotate;

        private Vector3 _directionRotate = new (0, 1, 0);

        private void Update()
        {
            transform.Rotate(_speedRotate * Time.deltaTime * _directionRotate);
        }
    }
}