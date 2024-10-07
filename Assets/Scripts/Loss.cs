using System.Collections;
using UnityEngine;
using YG.Example;

public class Loss : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TestFocus _testFocus;
    [SerializeField] private AliveNeutral _aliveNeutral;
    [SerializeField] private Player _player;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private NewResultLBExample _newResultLBExample;

    private Coroutine CancelDefeat;

    private void OnEnable()
    {
        _winScreen.Winning += CancelLoss;
        _player.ThrowsOver += DeclareLoss;
        _aliveNeutral.DeathNeutrals += DeclareLoss;
        _newResultLBExample.NewScore(_player.Score);
    }

    private void OnDisable()
    {
        _winScreen.Winning -= CancelLoss;
        _player.ThrowsOver -= DeclareLoss;
        _aliveNeutral.DeathNeutrals -= DeclareLoss;
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