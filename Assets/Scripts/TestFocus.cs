using UnityEngine;
using Agava.WebUtility;

public class TestFocus : MonoBehaviour
{
    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        if (value == false)
        {
            if (PlayerPrefs.HasKey("currentVolume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("currentVolume");
                return;
            }

            AudioListener.volume = 1;
        }

        if (value == true)
        {
            AudioListener.volume = 0;
        }
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
