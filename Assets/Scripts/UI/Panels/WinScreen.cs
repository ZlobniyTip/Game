using SDK;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private TestFocus _testFocus;
        [SerializeField] private ProgressBar _progressBar;

        private Coroutine _win;

        public event UnityAction Winning;

        private void OnEnable()
        {
            _progressBar.Win += StartWin;
        }

        private void OnDisable()
        {
            if (_win != null)
            {
                StopCoroutine(_win);
            }

            _progressBar.Win -= StartWin;
        }

        public void OpenWinScreen()
        {
            _testFocus.enabled = false;
            _winScreen.SetActive(true);

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }

        private void StartWin()
        {
            _win = StartCoroutine(Win());
        }

        private IEnumerator Win()
        {
            Winning?.Invoke();
            var delay = new WaitForSeconds(2);

            yield return delay;

            OpenWinScreen();
        }
    }
}