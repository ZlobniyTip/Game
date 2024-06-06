using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("currentVolume");
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}
