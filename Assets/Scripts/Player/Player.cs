using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerUseSkills _playerUseSkills;
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private Thrower _thrower;
    [SerializeField] private Loss _loss;
    [SerializeField] private PlayersWeapon _playersWeapon;
    [SerializeField] private SkinEditor _skinEditor;

    private int _maxNumberThrows = 4;

    public event UnityAction<int> MoneyChange;
    public event UnityAction<int, int> ThrowsChange;

    public Thrower Thrower => _thrower;
    public List<Skill> Skills => _skills;

    public int Money { get; private set; }
    public int RemainingNumThrows { get; private set; }
    public int Score { get; private set; }

    private void Start()
    {
        if (PlayerPrefs.HasKey("currentScore"))
        {
            Score = PlayerPrefs.GetInt("currentScore");
        }

        if (PlayerPrefs.HasKey("currentMoney"))
        {
            Money = PlayerPrefs.GetInt("currentMoney");
        }

        MoneyChange?.Invoke(Money);

        if (PlayerPrefs.HasKey("throwCount"))
        {
            _maxNumberThrows = PlayerPrefs.GetInt("throwCount");
        }

        RemainingNumThrows = _maxNumberThrows;
        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);

        Money = 10000;
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

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_skills[1].Used == false)
            {
                _playerUseSkills.GetSkill(_skills[1]);
            }
        }
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

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        weapon.WeaponState.Buy();
        _playersWeapon.ChangeWeapon(weapon);
    }

    public void BuySkin(Skin skin)
    {
        Money -= skin.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
        skin.SkinState.Buy();
        _skinEditor.ChangeSkin(skin);
    }
}
