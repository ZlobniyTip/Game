using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WindView : MonoBehaviour
{
    [SerializeField] private Wind _wind;
    [SerializeField] private Slider _windSlider;
    [SerializeField] private Sprite _windImageLeft;
    [SerializeField] private Sprite _windImageRigth;
    [SerializeField] private Image _windImage;
    [SerializeField] private float _recoveryRate;

    private Coroutine _windForce;

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
        _windForce = StartCoroutine(ChangeWindForce(target));
    }

    private IEnumerator ChangeWindForce(float target)
    {
        while (_windSlider.value != target)
        {
            _windSlider.value = Mathf.MoveTowards(_windSlider.value, target, _recoveryRate * Time.deltaTime);

            yield return null;
        }
    }

    private void ChangeWindDirection()
    {
        if (_windSlider.value >= 0.5f)
        {
            _windImage.sprite = _windImageRigth;
        }
        else
        {
            _windImage.sprite = _windImageLeft;
        }
    }
}