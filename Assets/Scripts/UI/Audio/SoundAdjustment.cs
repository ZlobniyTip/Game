using UnityEngine;
using UnityEngine.UI;

public class SoundAdjustment : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("currentVolume");
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _slider.value;
        PlayerPrefs.SetFloat("currentVolume", _slider.value);
    }
}