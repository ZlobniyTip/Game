using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Source.Yandex
{
    public sealed class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            OnCallGameReadyButtonClick();
        }

        //private IEnumerator Start()
        //{
        //    yield return YG.YandexGame.Initialize(OnInitialized);
        //}

        private void OnInitialized()
        {
            SceneManager.LoadScene(1);
        }

        public void OnCallGameReadyButtonClick()
        {
            //YandexGamesSdk.GameReady();
        }
    }
}