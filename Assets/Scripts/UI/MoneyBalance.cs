using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private Player _player;

    private void Start()
    {
        OnValueChanged(_player.Money);
    }

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
