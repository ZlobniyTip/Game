using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonus : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _buttonAddForce;
    [SerializeField] private Button _buttonAddForceX2;
    [SerializeField] private Button _buttonAddThrow;
    [SerializeField] private Button _buttonAddThrowX2;
    [SerializeField] private Button _nextLevel;
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private Teleportation _teleportation;
    [SerializeField] private TMP_Text _valueDefaultReward;
    [SerializeField] private TMP_Text _valueX2Reward;

    private float _forceValue = 0.5f;
    private int _forceValueX2 = 1;
    private int _countValue = 1;
    private int _countValueX2 = 2;

    private void OnDisable()
    {
        _videoAd.RewardCallback -= AddThrowForceX2;
        _videoAd.RewardCallback -= AddThrowCountX2;
    }

    private void OnEnable()
    {
        ShowChangeValue(_valueDefaultReward, _forceValue);
        ShowChangeValue(_valueX2Reward, _forceValueX2);
    }

    private void Start()
    {
        _player.Thrower.enabled = false;
        _teleportation.enabled = false;
        _player.AddScore(_player.RemainingNumThrows);
        _yandexLeaderboard.SetPlayerScore(_player.Score);
    }

    public void TryAddThrowCountX2()
    {
        _videoAd.RewardCallback += AddThrowCountX2;
        _videoAd.Show();
        _nextLevel.gameObject.SetActive(true);
        LockButton();
    }

    public void AddThrowCountX2()
    {
        _player.AddThrowCount(_countValueX2);
    }

    public void TryAddThrowForceX2()
    {
        _videoAd.RewardCallback += AddThrowForceX2;
        _videoAd.Show();
        _nextLevel.gameObject.SetActive(true);
        LockButton();
    }

    public void AddThrowForceX2()
    {
        _player.Thrower.AddThrowForce(_forceValueX2);
    }

    public void AddThrowCount()
    {
        _nextLevel.gameObject.SetActive(true);
        _player.AddThrowCount(_countValue);
        LockButton();
    }

    public void AddThrowForce()
    {
        _nextLevel.gameObject.SetActive(true);
        _player.Thrower.AddThrowForce(_forceValue);
        LockButton();
    }

    private void LockButton()
    {
        _buttonAddForceX2.gameObject.SetActive(false);
        _buttonAddForce.gameObject.SetActive(false);
        _buttonAddThrowX2.gameObject.SetActive(false);
        _buttonAddThrow.gameObject.SetActive(false);
    }

    private void ShowChangeValue(TMP_Text text,float value)
    {
        text.text = ($"{_player.Thrower.VelocityMult} > {_player.Thrower.VelocityMult + value} + ({value})");
    }
}