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

    private int _maxNumberThrows = 7;

    public event UnityAction<int> MoneyChange;
    public event UnityAction<int, int> ThrowsChange;

    public List<Skill> Skills => _skills;

    public int Money { get; private set; }
    public int RemainingNumThrows { get; private set; }

    private void Start()
    {
        if (PlayerPrefs.HasKey("currentMoney"))
        {
            Money = PlayerPrefs.GetInt("currentMoney");
        }

        MoneyChange?.Invoke(Money);
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
        PlayerPrefs.SetInt("currentMoney", Money);
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

        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);
    }

    public void GetReward(int reward)
    {
        Money += reward;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        PlayerPrefs.SetInt("currentMoney", Money);
        MoneyChange?.Invoke(Money);
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
