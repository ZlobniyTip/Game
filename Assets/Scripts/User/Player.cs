using Save;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace User
{
    [RequireComponent(typeof(PlayersWeapon))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerSkills _playerSkills;
        [SerializeField] private Thrower _thrower;
        [SerializeField] private PlayersWeapon _playersWeapon;
        [SerializeField] private PlayerSkins _skinEditor;
        [SerializeField] private Collider _collider;

        private readonly int _defaultNumThrows = 5;
        private int _maxNumberThrows = 5;

        public event UnityAction<int> MoneyChanged;
        public event UnityAction<int, int> ThrowsChanged;
        public event Action EquipmentChanged;
        public event Action CompletingThrow;

        public PlayerSkills PlayerSkills => _playerSkills;
        public Thrower Thrower => _thrower;
        public PlayerSkins SkinEditor => _skinEditor;
        public PlayersWeapon PlayersWeapon => _playersWeapon;
        public int MaxNumberThrows => _maxNumberThrows;

        public int Money { get; private set; }
        public int RemainingNumThrows { get; private set; }
        public int Score { get; private set; }

#if UNITY_EDITOR
        private void Start()
        {
            Money += 9000;
            MoneyChanged?.Invoke(Money);
        }
#endif

        public void UpdatePlayerInformation()
        {
            MoneyChanged?.Invoke(Money);

            RemainingNumThrows = _maxNumberThrows;
            ThrowsChanged?.Invoke(RemainingNumThrows, _maxNumberThrows);
        }

        public void LoadScore(int score)
        {
            Score = score;
        }

        public void LoadMoney(int money)
        {
            Money = money;
            MoneyChanged?.Invoke(Money);
        }

        public void LoadMaxNumThrows(int throws)
        {
            if (throws <= 0)
                _maxNumberThrows = _defaultNumThrows;
            else
                _maxNumberThrows = throws;

            ThrowsChanged?.Invoke(_maxNumberThrows, _maxNumberThrows);
        }

        public void ResetThrowCount()
        {
            _maxNumberThrows = _defaultNumThrows;
        }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void AddThrowCount(int value)
        {
            _maxNumberThrows += value;
        }

        public void GetReward(int reward)
        {
            Money += reward;
            AddScore(1);
            MoneyChanged?.Invoke(Money);
        }

        public void ReduceNumThrows()
        {
            RemainingNumThrows--;

            if (RemainingNumThrows == 0)
            {
                CompletingThrow?.Invoke();
                _collider.enabled = false;
            }

            ThrowsChanged?.Invoke(RemainingNumThrows, _maxNumberThrows);
        }

        public List<Product> TakeProductsList(ItemType type)
        {
            switch (type)
            {
                case ItemType.Weapon:
                    return _playersWeapon.Products;
                case ItemType.Skill:
                    return _playerSkills.Products;
                case ItemType.Skin:
                    return _skinEditor.Products;
                default:
                    return null;
            }
        }

        public bool TryPurchaseItem(Product product)
        {
            if (Money < product.Price)
                return false;

            Money -= product.Price;
            MoneyChanged?.Invoke(Money);
            product.State.SetStatus(ItemStatus.Purchased);
            EquipmentChanged?.Invoke();

            return true;
        }

        public void EquipItem(Product product, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    _playersWeapon.EquipWeapon(product);
                    break;
                case ItemType.Skill:
                    _playerSkills.EquipSkill(product);
                    break;
                case ItemType.Skin:
                    _skinEditor.EquipSkin(product);
                    break;
            }

            EquipmentChanged?.Invoke();
        }
    }
}