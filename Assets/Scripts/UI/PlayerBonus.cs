using UnityEngine;
using UnityEngine.UI;

public class PlayerBonus : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _buttonAddForce;
    [SerializeField] private Button _buttonAddThrow;
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;

    private int _forceValue = 1;
    private int _countValue = 1;

    private void Start()
    {
        _yandexLeaderboard.SetPlayerScore(_player.Score);
        _player.AddScore(_player.RemainingNumThrows);
    }

    public void AddThrowCount()
    {
        _player.AddThrowCount(_countValue);
        LockButton();
    }

    public void AddThrowForce()
    {
        _player.Thrower.AddThrowForce(_forceValue);
        LockButton();
    }

    private void LockButton()
    {
        _buttonAddForce.interactable = false;
        _buttonAddThrow.interactable = false;
    }
}