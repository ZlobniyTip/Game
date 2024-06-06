using Lean.Localization;
using UnityEngine;

public class LanguageChange : MonoBehaviour
{
    private const string RussianLanguage = "Russian";
    private const string EnglishLanguage = "English";
    private const string TurkishLanguage = "Turkish";

    [SerializeField] private LeanLocalization _leanLocalization;

    public void ChangeLanguageRussian()
    {
        _leanLocalization.SetCurrentLanguage(RussianLanguage);
    }

    public void ChangeLanguageEnglish()
    {
        _leanLocalization.SetCurrentLanguage(EnglishLanguage);
    }

    public void ChangeLanguageTurkish()
    {
        _leanLocalization.SetCurrentLanguage(TurkishLanguage);
    }
}
