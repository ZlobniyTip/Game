using UnityEngine;

public class Offset : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _offset;

    private bool _isActive = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Collision")
        {
            if (_isActive)
            {
                foreach (ContactPoint point in collision.contacts)
                {
                    Vector3 hitPoint = point.point;
                    transform.position = hitPoint + point.normal * _offset;
                }

                _rigidbody.isKinematic = true;
                ActivateOffset(false);
            }
        }
    }

    public void ActivateOffset(bool isActive)
    {
        _isActive = isActive;
    }
}
