using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowDefault : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void ResetThrowCount()
    {
        _player.ResetThrowCount();
        SceneManager.LoadScene(0);
    }
}