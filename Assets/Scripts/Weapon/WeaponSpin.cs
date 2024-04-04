using System.Collections;
using UnityEngine;

public class WeaponSpin : MonoBehaviour
{
    private Coroutine _spin;
    private float _speedRotation = 400;

    private void Start()
    {
        _spin = StartCoroutine(Spin());   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitRagdollBone bone))
        {
            StopCoroutine(_spin);
        }

        if (collision.gameObject.TryGetComponent(out ExplosiveBarrel explosiveBarrel))
        {
            StopCoroutine(_spin);
        }

        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            StopCoroutine(_spin);
        }
    }

    private IEnumerator Spin()
    {
        var torsionDuration = new WaitForSeconds(3);

        while (true)
        {
            transform.Rotate(_speedRotation * Time.deltaTime, 0, 0);

            yield return null;
        }
    }
}
