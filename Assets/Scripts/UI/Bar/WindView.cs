using Environment;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindView : MonoBehaviour
    {
        [SerializeField] private Wind _wind;
        [SerializeField] private Slider _windSlider;
        [SerializeField] private Sprite _windImageLeft;
        [SerializeField] private Sprite _windImageRigth;
        [SerializeField] private Image _windImage;
        [SerializeField] private float _recoveryRate;

        private void OnEnable()
        {
            _wind.ChangingWindDirection += ChangeWindDirection;
            _wind.ChangingWindForce += StartChangeWindForce;
        }

        private void OnDisable()
        {
            _wind.ChangingWindDirection -= ChangeWindDirection;
            _wind.ChangingWindForce -= StartChangeWindForce;
        }

        private void StartChangeWindForce(float target)
        {
            StartCoroutine(ChangeWindForce(target));
        }

        private IEnumerator ChangeWindForce(float target)
        {
            while (_windSlider.value != target)
            {
                _windSlider.value = Mathf.MoveTowards(_windSlider.value, target, _recoveryRate * Time.deltaTime);

                yield return null;
            }
        }

        private void ChangeWindDirection(Vector3 direction)
        {
            if (direction == Vector3.right)
            {
                _windImage.sprite = _windImageRigth;
            }
            else if (direction == Vector3.left)
            {
                _windImage.sprite = _windImageLeft;
            }
        }
    }
}