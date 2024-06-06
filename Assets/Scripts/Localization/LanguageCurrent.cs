using Lean.Localization;
using UnityEngine;

public class LanguageCurrent : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private void Start()
    {
        if (PlayerPrefs.HasKey("currentLanguage"))
        {
            string currentLanguage = PlayerPrefs.GetString("currentLanguage");

            _leanLocalization.SetCurrentLanguage(currentLanguage);
        }
    }
}
