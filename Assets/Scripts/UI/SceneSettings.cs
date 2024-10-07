using UnityEngine;
using UnityEngine.SceneManagement;
using YG.Example;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _menu;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private SaverTest _saverTest;

    private int _nextScene;

    private void OnDisable()
    {
        _videoAd.RewardCallback -= RestartScene;
    }

    private void Start()
    {
        _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        _saverTest.Save();
    }

    public void TryRestartScene()
    {
        _videoAd.RewardCallback += RestartScene;
        _videoAd.Show();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(_nextScene);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void StartFirstLevel()
    {
        SceneManager.LoadScene(2);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}