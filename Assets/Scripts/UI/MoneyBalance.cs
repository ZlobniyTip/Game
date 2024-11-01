using TMPro;
using UnityEngine;
using User;

namespace UI
{
    public class MoneyBalance : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private Player _player;

        public Player Player => _player;

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
            _player.MoneyChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _player.MoneyChanged -= OnValueChanged;
        }
    }
}