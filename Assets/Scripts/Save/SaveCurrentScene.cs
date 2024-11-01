using UnityEngine;
using UnityEngine.SceneManagement;

namespace Save
{
    public class SaveCurrentScene : MonoBehaviour
    {
        private readonly int _firstLevelSceneIndex = 1;
        private int _currentSceneIndex;

        private void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
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
}