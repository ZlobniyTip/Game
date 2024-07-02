using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TestFocus _testFocus;

    public void OpenWinScreen()
    {
        _testFocus.enabled = false;
        _winScreen.SetActive(true);

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }
}