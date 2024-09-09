using System.Collections;
using UnityEngine;

public class WeaponSpin : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private Coroutine _spin;
    private float _speedRotation = 400;
    private int _indexSai = 4;

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
            if (_weapon.Index == _indexSai)
            {
                transform.Rotate(0, 0, _speedRotation * Time.deltaTime);
                yield return null;
            }

            transform.Rotate(_speedRotation * Time.deltaTime, 0, 0);

            yield return null;
        }
    }
}
