using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private MoneyBalance _moneyBalance;
        [SerializeField] private ItemView _template;
        [SerializeField] private GameObject _itemContainer;
        [SerializeField] private ItemType _itemType;

        private readonly List<ItemView> _content = new ();

        private void OnEnable()
        {
            foreach (var item in _moneyBalance.Player.TakeProductsList(_itemType))
                AddItemView(item);
        }

        private void OnDisable()
        {
            foreach (var item in _content)
            {
                item.PurchaseButtonPressed -= OnPurchaseButtonPressed;
                item.EquipButtonPressed -= OnEquipButtonPressed;
                Destroy(item.gameObject);
            }

            _content.Clear();
        }

        private void AddItemView(Product product)
        {
            var view = Instantiate(_template, _itemContainer.transform);
            view.Init(product);
            view.PurchaseButtonPressed += OnPurchaseButtonPressed;
            view.EquipButtonPressed += OnEquipButtonPressed;
            _content.Add(view);
        }

        private void OnPurchaseButtonPressed(ItemView view)
        {
            _moneyBalance.Player.TryPurchaseItem(view.Product);
        }

        private void OnEquipButtonPressed(ItemView view)
        {
            _moneyBalance.Player.EquipItem(view.Product, _itemType);
        }
    }
}