using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] protected Image _barFilling;
        [SerializeField] private TMP_Text _text;

        private readonly float _recoveryRate = 0.2f;
        private Coroutine _changeValue;

        public void OnValueChanged(int value, int maxValue)
        {
            if (_changeValue != null)
            {
                StopCoroutine(_changeValue);
            }

            _text.text = $"{value} / {maxValue}";
            _changeValue = StartCoroutine(ChangeHealthBar((float)value / maxValue));
        }

        private IEnumerator ChangeHealthBar(float target)
        {
            while (_barFilling.fillAmount != target)
            {
                _barFilling.fillAmount = Mathf.MoveTowards(_barFilling.fillAmount, target, _recoveryRate * Time.deltaTime);

                yield return null;
            }

            yield break;
        }
    }
}