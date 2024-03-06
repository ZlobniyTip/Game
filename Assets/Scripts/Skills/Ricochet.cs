using UnityEngine;

public class Ricochet : MonoBehaviour
{
    [SerializeField] private float _ricochetForce;

    public void CalculateRicochet(Rigidbody weapon, Ray ray, RaycastHit raycastHit)
    {
        weapon.AddForce(Vector3.Reflect(ray.direction, raycastHit.normal));
    }
}
