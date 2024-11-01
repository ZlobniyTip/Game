using Advertisement;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG.Example;

namespace UI
{
    public class SceneSettings : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _menu;
        [SerializeField] private VideoAd _videoAd;
        [SerializeField] private SaverTest _saverTest;

        private int _nextScene;

        private void OnDisable()
        {
            _videoAd.RewardedCallback -= RestartScene;
        }

        private void Start()
        {
            _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        }

        public void TryRestartScene()
        {
            _videoAd.RewardedCallback += RestartScene;
            _videoAd.Show();
        }

        public void RestartScene()
        {
            _saverTest.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void ChangeScene()
        {
            _saverTest.Save();
            SceneManager.LoadScene(_nextScene);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        public void StartFirstLevel()
        {
            _saverTest.Save();
            SceneManager.LoadScene(2);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}