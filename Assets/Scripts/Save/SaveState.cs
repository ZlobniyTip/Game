using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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

        _savePath = Path.Combine(Application.dataPath, _saveFileName);
    }

    public void SaveFile()
    {
        GameCoreStruct gameCoreStruct = new GameCoreStruct
        {
            weaponStates = this._weaponStates,
            skinStates = this._skinStates,
            skillStates = this._skillStates
        };

        string json = JsonUtility.ToJson(gameCoreStruct, prettyPrint: true);

        try
        {
            File.WriteAllText(_savePath, contents: json);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void LoadFile()
    {
        if (!File.Exists(_savePath))
        {
            return;
        }

        try
        {
            string json = File.ReadAllText(_savePath);

            GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

            this._weaponStates = gameCoreFromJson.weaponStates;
            this._skinStates = gameCoreFromJson.skinStates;
            this._skillStates = gameCoreFromJson.skillStates;
        }
        catch (Exception)
        {
            throw;
        }
    }
}