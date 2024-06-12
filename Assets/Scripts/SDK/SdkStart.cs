using Agava.YandexGames;
using UnityEngine;

public class SdkStart : MonoBehaviour
{
    private void Start()
    {
        OnCallGameReadyButtonClick();
    }

    public void OnCallGameReadyButtonClick()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        YandexGamesSdk.GameReady();
#endif 
    }
}
