using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private Player _player;

    public void OnValueChanged(int value)
    {
        _coins.text = value.ToString();
    }

    private void OnEnable()
    {
        _player.MoneyChange += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChange -= OnValueChanged;
    }
}
