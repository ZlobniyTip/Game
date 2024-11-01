using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundAdjustment : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private readonly float _defaultSound = 0.5f;

        private void Start()
        {
            if (PlayerPrefs.HasKey("currentVolume"))
            {
                _slider.value = PlayerPrefs.GetFloat("currentVolume");
                AudioListener.volume = _slider.value;
            }
            else
            {
                _slider.value = _defaultSound;
                AudioListener.volume = _defaultSound;
            }
        }

        public void ChangeVolume()
        {
            AudioListener.volume = _slider.value;
            PlayerPrefs.SetFloat("currentVolume", _slider.value);
        }
    }
}