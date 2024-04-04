using UnityEngine;
using UnityEngine.UI;

public class SoundAdjustment : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void ChangeVolume()
    {
        AudioListener.volume = _slider.value;
    }
}