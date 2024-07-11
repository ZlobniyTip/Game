using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _menu;
    [SerializeField] private SaveState _saveState;
    [SerializeField] private VideoAd _videoAd;

    private int _nextScene;

    private void Start()
    {
        _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void ExitToMenu()
    {
        _saveState.SaveFile();
        _menu.SetActive(true);
        _winScreen.SetActive(false);
    }

    public void RestartScene()
    {
        _saveState.SaveFile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        _videoAd.Show();
    }

    public void ChangeScene()
    {
        _saveState.SaveFile();
        SceneManager.LoadScene(_nextScene);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void StartFirstLevel()
    {
        _saveState.SaveFile();
        SceneManager.LoadScene(3);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}