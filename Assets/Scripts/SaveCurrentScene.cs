using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCurrentScene : MonoBehaviour
{
    private int _firstLevelSceneIndex = 2;
    private int _currentSceneIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2)
        {
            SaveSceneIndex();
        }
    }

    private void SaveSceneIndex()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("currentScene", _currentSceneIndex);
    }

    public void LoadSceneIndex()
    {
        if (PlayerPrefs.HasKey("currentScene"))
        {
            _currentSceneIndex = PlayerPrefs.GetInt("currentScene");
            SceneManager.LoadScene(_currentSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(_firstLevelSceneIndex);
        }

    }
}
