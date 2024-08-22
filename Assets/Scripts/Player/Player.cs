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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_skills[0].Used == false)
            {
                _playerUseSkills.GetSkill(_skills[0]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_skills[1].Used == false)
            {
                _playerUseSkills.GetSkill(_skills[1]);
            }
        }
    }

    public void StartEvents()
    {
        MoneyChange?.Invoke(Money);

        RemainingNumThrows = _maxNumberThrows;
        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);
    }

    public void LoadMoney(int money)
    {
        Money = money;
    }

    public void LoadMaxNumThrows(int throws)
    {
        _maxNumberThrows = throws;
    }

    public void LoadSkill(List<SkillState> skills)
    {
        for (int i = 0; i < _skills.Count; i++)
        {
            _skills[i].SkillState.LoadState(skills[i].IsBuying);
        }
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

    public void BuySkill(Skill skill)
    {
        Money -= skill.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        skill.SkillState.Buy();
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

    public void BuySkin(Skin skin)
    {
        Money -= skin.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        skin.SkinState.Buy();
        _skinEditor.ChangeSkin(skin);
        
        EquipmentChanged?.Invoke();
    }
}