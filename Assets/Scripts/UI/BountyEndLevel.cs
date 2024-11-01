using UnityEngine;
using UnityEngine.SceneManagement;
using User;

namespace UI
{
    public class BountyEndLevel : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private readonly int _reward = 20000;

        public void GetReward()
        {
            _player.GetReward(_reward);

            SceneManager.LoadScene(1);
        }
    }
}