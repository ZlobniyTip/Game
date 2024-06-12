using System.Collections.Generic;
using UnityEngine;
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
            skillStates = _skillStates
        };

        string json = JsonUtility.ToJson(gameCoreStruct, prettyPrint: true);

        Debug.Log(json);

        PlayerPrefs.SetString("save", json);
    }

    public void LoadFile()
    {
        string json;

        if (PlayerPrefs.HasKey("save"))
        {
            json = PlayerPrefs.GetString("save");
        }
        else
        {
            return;
        }

        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        _player.LoadSkill(gameCoreFromJson.skillStates);
        _playersWeapon.LoadWeapons(gameCoreFromJson.weaponStates);
        _skinEditor.LoadSkins(gameCoreFromJson.skinStates);
    }
}