using UnityEngine;
using YG;

public class SdkStart : MonoBehaviour
{
    private void Start()
    {
        OnCallGameReadyButtonClick();
    }

    public void OnCallGameReadyButtonClick()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        YandexGame.GameReadyAPI();
#endif 
    }
}
