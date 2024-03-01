using UnityEngine;

public class ThrowCounter : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ThrowsChange += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ThrowsChange -= OnValueChanged;
    }
}
