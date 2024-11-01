using UnityEngine;
using User;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private PlayerActivitySwitch _player;

        private void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("currentVolume");
        }

        public void OpenPanel(GameObject panel)
        {
            _player.Switching(true);
            panel.SetActive(true);

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }

        public void ClosePanel(GameObject panel)
        {
            _player.Switching(false);
            panel.SetActive(false);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}