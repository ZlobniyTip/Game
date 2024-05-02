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
        _videoAd.Show();
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
        Time.timeScale = 1;
    }

    public void ChangeScene()
    {
        _saveState.SaveFile();
        SceneManager.LoadScene(_nextScene);
        Time.timeScale = 1;
    }
}