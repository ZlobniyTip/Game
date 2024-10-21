using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public event Action<int> LoadData;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += GetLoad;
            _player.EquipmentChanged += Save;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= GetLoad;
            _player.EquipmentChanged -= Save;
        }

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
        }

        public void Save()
        {
            for (int i = 0; i < _player.PlayersWeapon.Weapons.Count; i++)
            {
                YandexGame.savesData.weaponStates[i] = _player.PlayersWeapon.Weapons[i].State.Status;
            }

            for (int i = 0; i < _player.SkinEditor.Skins.Count; i++)
            {
                YandexGame.savesData.skinStates[i] = _player.SkinEditor.Skins[i].State.Status;
            }

            for (int i = 0; i < _player.Skills.Count; i++)
            {
                YandexGame.savesData.skillStates[i] = _player.Skills[i].State.Status;
            }

            YandexGame.savesData.playerMoney = _player.Money;
            YandexGame.savesData.maxNumberThrows = _player.MaxNumberThrows;
            YandexGame.savesData.velocityMult = _player.Thrower.VelocityMult;
            YandexGame.savesData.score = _player.Score;

            YandexGame.SaveProgress();
        }

        public void GetLoad()
        {
            _player.PlayersWeapon.InitWeapons(YandexGame.savesData.weaponStates);
            _player.SkinEditor.InitSkins(YandexGame.savesData.skinStates);
            _player.InitSkills(YandexGame.savesData.skillStates);

            _player.LoadScore(YandexGame.savesData.score);
            _player.LoadMoney(YandexGame.savesData.playerMoney);
            _player.LoadMaxNumThrows(YandexGame.savesData.maxNumberThrows);
            _player.Thrower.LoadVelocityMult(YandexGame.savesData.velocityMult);

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _player.ResetThrowCount();
                _player.Thrower.ResetThrowForce();
            }

            _player.StartEvents();
            LoadData?.Invoke(_player.Score);
        }
    }
}