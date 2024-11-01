using Enemy;
using SDK;
using System.Collections;
using UI;
using UnityEngine;
using YG.Example;

namespace User
{
    public class Loss : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TestFocus _testFocus;
        [SerializeField] private NeutralCounter _aliveNeutral;
        [SerializeField] private Player _player;
        [SerializeField] private WinScreen _winScreen;
        [SerializeField] private NewResultLBExample _newResultLBExample;

        private Coroutine CancelDefeat;

        private void OnEnable()
        {
            _winScreen.Winning += CancelLoss;
            _player.CompletingThrow += DeclareLoss;
            _aliveNeutral.MurderedNeutral += DeclareLoss;
            _newResultLBExample.NewScore(_player.Score);
        }

        private void OnDisable()
        {
            _winScreen.Winning -= CancelLoss;
            _player.CompletingThrow -= DeclareLoss;
            _aliveNeutral.MurderedNeutral -= DeclareLoss;
            StopCoroutine(OpenLossScreen());
        }

        private void CancelLoss()
        {
            if (CancelDefeat != null)
            {
                StopCoroutine(CancelDefeat);
            }
        }

        private void DeclareLoss()
        {
            _testFocus.enabled = false;
            CancelDefeat = StartCoroutine(OpenLossScreen());
        }

        private IEnumerator OpenLossScreen()
        {
            yield return new WaitForSeconds(2);

            _panel.SetActive(true);

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }
    }
}