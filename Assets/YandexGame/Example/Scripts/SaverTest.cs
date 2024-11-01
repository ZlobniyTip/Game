using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using User;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public event Action<int> LoadedData;

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
            for (int i = 0; i < _player.PlayersWeapon.Products.Count; i++)
            {
                YandexGame.savesData.weaponStates[i] = _player.PlayersWeapon.Products[i].State.Status;
            }

            for (int i = 0; i < _player.SkinEditor.Products.Count; i++)
            {
                YandexGame.savesData.skinStates[i] = _player.SkinEditor.Products[i].State.Status;
            }

            for (int i = 0; i < _player.PlayerSkills.Products.Count; i++)
            {
                YandexGame.savesData.skillStates[i] = _player.PlayerSkills.Products[i].State.Status;
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
            _player.PlayerSkills.InitSkills(YandexGame.savesData.skillStates);

            _player.LoadScore(YandexGame.savesData.score);
            _player.LoadMoney(YandexGame.savesData.playerMoney);
            _player.LoadMaxNumThrows(YandexGame.savesData.maxNumberThrows);
            _player.Thrower.LoadVelocityMult(YandexGame.savesData.velocityMult);

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _player.ResetThrowCount();
                _player.Thrower.ResetThrowForce();
            }

            _player.UpdatePlayerInformation();
            LoadedData?.Invoke(_player.Score);
        }
    }
}