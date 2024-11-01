using Enemy;
using Environment;
using System.Collections;
using UnityEngine;

namespace User
{
    public class WeaponSpin : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private readonly float _speedRotation = 400;
        private Coroutine _spin;

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
            while (true)
            {
                transform.Rotate(_speedRotation * Time.deltaTime, 0, 0);

                yield return null;
            }
        }
    }
}