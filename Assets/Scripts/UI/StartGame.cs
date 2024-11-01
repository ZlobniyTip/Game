using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;

        public void Play()
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}