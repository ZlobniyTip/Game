using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    [SerializeField] private Player _player;

    private List<ItemState> _weaponStates = new();
    private List<ItemState> _skinStates = new();
    private List<ItemState> _skillStates = new();

    private void Awake()
    {
        _player.EquipmentChanged += OnEquipmentChanged;

        foreach (var weapon in _player.PlayersWeapon.Weapons)
            _weaponStates.Add(weapon.State);

        foreach (var skin in _player.SkinEditor.Skins)
            _skinStates.Add(skin.State);

        foreach (var skill in _player.Skills)
            _skillStates.Add(skill.State);

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
            score = _player.Score,
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
            maxNumberThrows = _player.DefaultNumThrows,
            score = 0,
            velocityMult = _player.Thrower.VelocityMultDefault
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

    public void SynchronizeSaves()
    {
        PlayerAccount.GetCloudSaveData(SynchronizeData, StartEvents);
    }

    private void SynchronizeData(string json)
    {
        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        _player.LoadMaxNumThrows(gameCoreFromJson.maxNumberThrows);
        _player.LoadMoney(gameCoreFromJson.playerMoney);
        _player.LoadScore(gameCoreFromJson.score);
        _player.Thrower.LoadVelocityMult(gameCoreFromJson.velocityMult);

        SynchronizeItems(gameCoreFromJson.skillStates, _skillStates);
        SynchronizeItems(gameCoreFromJson.weaponStates, _weaponStates);
        SynchronizeItems(gameCoreFromJson.skinStates, _skinStates);

        _player.SetCurrentItems();
    }

    private void LoadLocalData(string json)
    {
        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        _player.LoadMaxNumThrows(gameCoreFromJson.maxNumberThrows);
        _player.LoadMoney(gameCoreFromJson.playerMoney);
        _player.LoadScore(gameCoreFromJson.score);
        _player.Thrower.LoadVelocityMult(gameCoreFromJson.velocityMult);

        _player.InitSkills(gameCoreFromJson.skillStates);
        _player.PlayersWeapon.InitWeapons(gameCoreFromJson.weaponStates);
        _player.SkinEditor.InitSkins(gameCoreFromJson.skinStates);

        ResetPlayerStatus();

        StartEvents(null);
    }

    private void SynchronizeItems(List<ItemState> localItems, List<ItemState> cloudItems)
    {
        for (int i = 0; i < cloudItems.Count; i++)
        {
            if (localItems[i].Status == ItemStatus.Purchased || localItems[i].Status == ItemStatus.Equipped)
            {
                cloudItems[i].Status = localItems[i].Status;
            }
        }
    }

    private void ResetPlayerStatus()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            _player.ResetThrowCount();
            _player.Thrower.ResetThrowForce();
        }
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