using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    public void Play()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}