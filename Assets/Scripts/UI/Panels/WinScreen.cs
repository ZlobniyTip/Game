using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;

    public void OpenWinScreen()
    {
        _winScreen.SetActive(true);

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }
}