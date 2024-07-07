using UnityEngine;
using UnityEngine.SceneManagement;

public class BountyEndLevel : MonoBehaviour
{
    [SerializeField] private Player _player;

    private int _reward = 20000;

    public void GetReward()
    {
        _player.GetReward(_reward);

        SceneManager.LoadScene(1);
    }
}
