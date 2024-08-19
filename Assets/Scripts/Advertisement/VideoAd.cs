using UnityEngine;
using UnityEngine.Events;

public class VideoAd : MonoBehaviour
{
    public event UnityAction RewardCallback;

    public void Show() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    private void OnOpenCallback()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0f;
    }

    private void OnRewardCallback()
    {
        RewardCallback?.Invoke();
    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1f;
    }
}