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
    [SerializeField] private Loss _loss;
    [SerializeField] private PlayersWeapon _playersWeapon;
    [SerializeField] private SkinEditor _skinEditor;

    private int _maxNumberThrows = 4;
    private int _defaultNumThrows = 4;

    public event UnityAction<int> MoneyChange;
    public event UnityAction<int, int> ThrowsChange;
    public event Action EquipmentChanged;

    public Thrower Thrower => _thrower;
    public SkinEditor SkinEditor => _skinEditor;
    public PlayersWeapon PlayersWeapon => _playersWeapon;
    public PlayerUseSkills PlayerUseSkills => _playerUseSkills;
    public List<Skill> Skills => _skills;
    public int MaxNumberThrows => _maxNumberThrows;

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

    public void InitSkills(List<SkillState> skills)
    {
        for (int i = 0; i < _skills.Count; i++)
            _skills[i].Init(skills[i]);
    }

    public void LoadMoney(int money)
    {
        Money = money;
    }

    public void LoadMaxNumThrows(int throws)
    {
        _maxNumberThrows = throws;
    }

    public void ResetThrowCount()
    {
        _maxNumberThrows = _defaultNumThrows;
        PlayerPrefs.SetInt("throwCount", _maxNumberThrows);
    }

    public void AddScore(int score)
    {
        Score += score;
        PlayerPrefs.SetInt("currentScore", Score);
    }

    public void AddThrowCount(int value)
    {
        _maxNumberThrows += value;
        PlayerPrefs.SetInt("throwCount", _maxNumberThrows);
    }

    public bool TryPurchaseSkill(Skill skill)
    {
        if (Money < skill.Price)
            return false;

        Money -= skill.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        skill.State.SetStatus(SkillStatus.Purchased);

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
            _loss.DeclareLoss();
            _thrower.enabled = false;
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
        weapon.State.SetStatus(WeaponStatus.Purchased);
        
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
        skin.State.SetStatus(SkinStatus.Purchased);

        EquipmentChanged?.Invoke();

        return true;
    }

    public void EquipSkin(Skin skin)
    {
        _skinEditor.EquipSkin(skin);

        EquipmentChanged?.Invoke();
    }
}