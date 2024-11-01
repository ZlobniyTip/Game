using UnityEngine;
using User;

namespace UI
{
    public class ThrowCounter : Bar
    {
        [SerializeField] private Player _player;

        private void OnEnable()
        {
            _player.ThrowsChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _player.ThrowsChanged -= OnValueChanged;
        }
    }
}