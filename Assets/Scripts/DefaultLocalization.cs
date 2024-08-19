using UnityEngine;

public class DefaultLocalization : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("currentLanguage"))
        {
            PlayerPrefs.DeleteKey("currentLanguage");
        }
    }
}