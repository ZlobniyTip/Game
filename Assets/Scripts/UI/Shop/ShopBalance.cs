using TMPro;
using UnityEngine;
using User;

namespace UI
{
    public class ShopBalance : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TMP_Text _balance;

        public void ShowBalance()
        {
            _balance.text = _player.Money.ToString();
        }
    }
}