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
        YandexGamesSdk.GameReady();
    }
}
