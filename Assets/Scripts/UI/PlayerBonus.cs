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
    [SerializeField] private Button _replay;
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private Teleportation _teleportation;

    private float _forceValue = 0.5f;
    private int _forceValueX2 = 1;
    private int _countValue = 1;
    private int _countValueX2 = 1;

    private void Start()
    {
        _player.Thrower.enabled = false;
        _teleportation.enabled = false;
        _nextLevel.interactable = false;
        _replay.interactable = false;
        _yandexLeaderboard.SetPlayerScore(_player.Score);
        _player.AddScore(_player.RemainingNumThrows);
    }

    public void AddThrowCountX2()
    {
        _replay.interactable = true;
        _nextLevel.interactable = true;
        _videoAd.Show();
        _player.AddThrowCount(_countValueX2);
        LockButton();
    }

    public void AddThrowCount()
    {
        _replay.interactable = true;
        _nextLevel.interactable = true;
        _player.AddThrowCount(_countValue);
        LockButton();
    }

    public void AddThrowForceX2()
    {
        _replay.interactable = true;
        _nextLevel.interactable = true;
        _videoAd.Show();
        _player.Thrower.AddThrowForce(_forceValueX2);
        LockButton();
    }

    public void AddThrowForce()
    {
        _replay.interactable = true;
        _nextLevel.interactable = true;
        _player.Thrower.AddThrowForce(_forceValue);
        LockButton();
    }

    private void LockButton()
    {
        _buttonAddForceX2.interactable = false;
        _buttonAddForce.interactable = false;
        _buttonAddThrowX2.interactable = false;
        _buttonAddThrow.interactable = false;
    }
}