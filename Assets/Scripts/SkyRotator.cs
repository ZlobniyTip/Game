using UnityEngine;

public class SkyRotator : MonoBehaviour
{
    [SerializeField] private float _speedRotate;

    private Vector3 _directionRotate = new Vector3(0,1,0);

    private void Update()
    {
        transform.Rotate(_directionRotate * _speedRotate * Time.deltaTime);
    }
}
