using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerUseSkills _playerUseSkills;
    [SerializeField] private Skill[] _skills;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Thrower _thrower;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private DataSave _save;
    [SerializeField] private Loss _loss;

    private int _maxNumberThrows = 7;

    public event UnityAction<int> MoneyChange;
    public event UnityAction<int,int> ThrowsChange;

    public int Money { get; private set; }
    public int RemainingNumThrows { get; private set; }

    private void Start()
    {
        Money = PlayerPrefs.GetInt("currentMoney");
        MoneyChange?.Invoke(Money);

        _thrower.GiveWeapon(_currentWeapon);
        RemainingNumThrows = _maxNumberThrows;
        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_skills[0].Used == false)
            {
                _playerUseSkills.GetSkill(_skills[0]);
            }
        }
    }

    public void BuySkill(int price)
    {
        Money -= price;
        MoneyChange?.Invoke(Money);
    }

    public void ReduceNumThrows()
    {
        RemainingNumThrows--;

        if (RemainingNumThrows == 0)
        {
            _loss.DeclareLoss();
            _thrower.enabled = false;
        }

        ThrowsChange?.Invoke(RemainingNumThrows,_maxNumberThrows);
    }

    public void GetReward(int reward)
    {
        Money += reward;
        _save.Save();
        MoneyChange?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChange?.Invoke(Money);
        _weapons.Add(weapon);
        _thrower.GiveWeapon(weapon);
        _currentWeapon = weapon;
    }
}
