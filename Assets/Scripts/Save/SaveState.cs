using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    [SerializeField] private PlayersWeapon _playersWeapon;
    [SerializeField] private SkinEditor _skinEditor;
    [SerializeField] private Player _player;

    [Header("SaveConfig")]
    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "data.json";

    private List<WeaponState> _weaponStates = new List<WeaponState>();
    private List<SkinState> _skinStates = new List<SkinState>();
    private List<SkillState> _skillStates = new List<SkillState>();

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);

        _weaponStates.Clear();
        _skillStates.Clear();
        _skinStates.Clear();

        LoadFile();

        for (int i = 0; i < _playersWeapon.Weapons.Count; i++)
        {
            _weaponStates.Add(_playersWeapon.Weapons[i].WeaponState);
        }

        for (int i = 0; i < _skinEditor.Skins.Count; i++)
        {
            _skinStates.Add(_skinEditor.Skins[i].SkinState);
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

    public void LoadFile()
    {
        PlayerAccount.GetCloudSaveData(LoadLocalData, StartEvents);
    }

    private void LoadLocalData(string json)
    {
        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        _player.LoadSkill(gameCoreFromJson.skillStates);
        _playersWeapon.LoadWeapons(gameCoreFromJson.weaponStates);
        _skinEditor.LoadSkins(gameCoreFromJson.skinStates);
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