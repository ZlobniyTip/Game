using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    [SerializeField] private Player _player;

    private List<WeaponState> _weaponStates = new();
    private List<SkinState> _skinStates = new();
    private List<SkillState> _skillStates = new();

    private void Awake()
    {
        _player.EquipmentChanged += OnEquipmentChanged;
        
        foreach (var weapon in _player.PlayersWeapon.Weapons) 
            _weaponStates.Add(weapon.State);
        
        foreach (var skin in _player.SkinEditor.Skins) 
            _skinStates.Add(skin.SkinState);

        foreach (var skill in _player.Skills) 
            _skillStates.Add(skill.SkillState);
        
        LoadFile();
    }

    private void OnDestroy()
    {
        _player.EquipmentChanged -= OnEquipmentChanged;
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
        
        string json = JsonUtility.ToJson(gameCoreStruct, prettyPrint: false);
        

#if UNITY_EDITOR
        PlayerPrefs.SetString("EditorSave", json);
        return;
#endif
        
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

        string json = JsonUtility.ToJson(gameCoreStruct, prettyPrint: false);

#if UNITY_EDITOR
        PlayerPrefs.SetString("EditorSave", json);
        LoadFile();
        return;
#endif
        
        PlayerAccount.SetCloudSaveData(json);
        LoadFile();
    }

    public void LoadFile()
    {
#if UNITY_EDITOR
        if (PlayerPrefs.HasKey("EditorSave") == false)
            SaveFile();
        
        LoadLocalData(PlayerPrefs.GetString("EditorSave"));
        return;
#endif
        
        PlayerAccount.GetCloudSaveData(LoadLocalData, StartEvents);
    }

    private void LoadLocalData(string json)
    {
        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        _player.LoadSkill(gameCoreFromJson.skillStates);
        _player.PlayersWeapon.InitWeapons(gameCoreFromJson.weaponStates);
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

    private void OnEquipmentChanged()
    {
        SaveFile();
    }
}