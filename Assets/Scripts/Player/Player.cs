using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayersWeapon))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerUseSkills _playerUseSkills;
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private Thrower _thrower;
    [SerializeField] private PlayersWeapon _playersWeapon;
    [SerializeField] private SkinEditor _skinEditor;
    [SerializeField] private Collider _collider;

    private int _maxNumberThrows = 5;
    private int _defaultNumThrows = 5;

    public event UnityAction<int> MoneyChange;
    public event UnityAction<int, int> ThrowsChange;
    public event Action EquipmentChanged;
    public event Action ThrowsOver;

    public Thrower Thrower => _thrower;
    public SkinEditor SkinEditor => _skinEditor;
    public PlayersWeapon PlayersWeapon => _playersWeapon;
    public PlayerUseSkills PlayerUseSkills => _playerUseSkills;
    public List<Skill> Skills => _skills;
    public int MaxNumberThrows => _maxNumberThrows;
    public int DefaultNumThrows => _defaultNumThrows;

    public int Money { get; private set; }
    public int RemainingNumThrows { get; private set; }
    public int Score { get; private set; }

#if UNITY_EDITOR
    private void Start()
    {
        Money += 9000;
        MoneyChange?.Invoke(Money);
    }
#endif

    public void StartEvents()
    {
        MoneyChange?.Invoke(Money);

        RemainingNumThrows = _maxNumberThrows;
        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);
    }

    public void InitSkills(List<ItemStatus> skills)
    {
        for (int i = 0; i < _skills.Count; i++)
        {
            _skills[i].Init(skills[i]);
        }
    }

    public void LoadMoney(int money)
    {
        Money = money;
        MoneyChange?.Invoke(Money);
    }

    public void LoadMaxNumThrows(int throws)
    {
        if (throws <= 0)
            _maxNumberThrows = _defaultNumThrows;
        else
            _maxNumberThrows = throws;

        ThrowsChange?.Invoke(_maxNumberThrows,_maxNumberThrows);
    }

    public void LoadScore(int score)
    {
        Score = score;
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

    public bool TryPurchaseSkill(Skill skill)
    {
        if (Money < skill.Price)
            return false;

        Money -= skill.Price;
        MoneyChange?.Invoke(Money);
        skill.State.SetStatus(ItemStatus.Purchased);

        EquipmentChanged?.Invoke();

        return true;
    }

    public void EquipSkill(Skill skill)
    {
        _playerUseSkills.EquipSkill(skill);

        EquipmentChanged?.Invoke();
    }

    public void ReduceNumThrows()
    {
        RemainingNumThrows--;

        if (RemainingNumThrows == 0)
        {
            ThrowsOver?.Invoke();
            _collider.enabled = false;
        }

        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);
    }

    public void GetReward(int reward)
    {
        Money += reward;
        AddScore(1);
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
    }

    public bool TryPurchaseWeapon(Weapon weapon)
    {
        if (Money < weapon.Price)
            return false;

        Money -= weapon.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        weapon.State.SetStatus(ItemStatus.Purchased);

        EquipmentChanged?.Invoke();

        return true;
    }

    public void EquipWeapon(Weapon weapon)
    {
        _playersWeapon.EquipWeapon(weapon);

        EquipmentChanged?.Invoke();
    }

    public bool TryPurchaseSkin(Skin skin)
    {
        if (Money < skin.Price)
            return false;

        Money -= skin.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        skin.State.SetStatus(ItemStatus.Purchased);

        EquipmentChanged?.Invoke();

        return true;
    }

    public void EquipSkin(Skin skin)
    {
        _skinEditor.EquipSkin(skin);

        EquipmentChanged?.Invoke();
    }
}