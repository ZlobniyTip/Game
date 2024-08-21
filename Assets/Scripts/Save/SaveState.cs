using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    [SerializeField] private Player _player;

    private List<WeaponState> _weaponStates = new List<WeaponState>();
    private List<SkinState> _skinStates = new List<SkinState>();
    private List<SkillState> _skillStates = new List<SkillState>();

    private void Awake()
    {
        _weaponStates.Clear();
        _skillStates.Clear();
        _skinStates.Clear();

        LoadFile();

        for (int i = 0; i < _player.PlayersWeapon.Weapons.Count; i++)
        {
            _weaponStates.Add(_player.PlayersWeapon.Weapons[i].WeaponState);
        }

        for (int i = 0; i < _player.SkinEditor.Skins.Count; i++)
        {
            _skinStates.Add(_player.SkinEditor.Skins[i].SkinState);
        }

        for (int i = 0; i < _player.Skills.Count; i++)
        {
            _skillStates.Add(_player.Skills[i].SkillState);
        }
    }

    public void SaveFile()
    {
        GameCoreStruct gameCoreStruct = new GameCoreStruct
        {
            weaponStates = _weaponStates,
            skinStates = _skinStates,
            skillStates = _skillStates,
            playerMoney = _player.Money,
            maxNumberThrows = _player.MaxNumberThrows,
            velocityMult = _player.Thrower.VelocityMult
        };

        string json = JsonUtility.ToJson(gameCoreStruct, prettyPrint: true);

        PlayerAccount.SetCloudSaveData(json);
    }

    public void RemoveAllSave()
    {
        GameCoreStruct gameCoreStruct = new GameCoreStruct
        {
            weaponStates = null,
            skinStates = null,
            skillStates = null,
            playerMoney = 0,
            maxNumberThrows = 4,
            velocityMult = 12
        };

        string json = JsonUtility.ToJson(gameCoreStruct, prettyPrint: true);

        PlayerAccount.SetCloudSaveData(json);

        LoadFile();
    }

    public void LoadFile()
    {
        PlayerAccount.GetCloudSaveData(LoadLocalData, StartEvents);
    }

    private void LoadLocalData(string json)
    {
        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        _player.LoadSkill(gameCoreFromJson.skillStates);
        _player.PlayersWeapon.LoadWeapons(gameCoreFromJson.weaponStates);
        _player.SkinEditor.LoadSkins(gameCoreFromJson.skinStates);
        _player.LoadMaxNumThrows(gameCoreFromJson.maxNumberThrows);
        _player.LoadMoney(gameCoreFromJson.playerMoney);
        _player.Thrower.LoadVelocityMult(gameCoreFromJson.velocityMult);

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            _player.ResetThrowCount();
            _player.Thrower.ResetThrowForce();
        }

        StartEvents(null);
    }

    private void StartEvents(string error)
    {
        _player.StartEvents();
    }
}