using Lean.Localization;
using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class Localization : MonoBehaviour
    {
        private const string EnglishCode = "English";
        private const string RussianCode = "Russian";
        private const string TurkishCode = "Turkish";
        private const string Turkish = "tr";
        private const string Russian = "ru";
        private const string English = "en";

        [SerializeField] private LeanLocalization _leanLocalization;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            if (PlayerPrefs.HasKey("currentLanguage"))
            {
                string currentLanguage = PlayerPrefs.GetString("currentLanguage");

                _leanLocalization.SetCurrentLanguage(currentLanguage);

                return;
            }

            string languageCode = YandexGame.EnvironmentData.language;

            switch (languageCode)
            {
                case English:
                    _leanLocalization.SetCurrentLanguage(EnglishCode);
                    PlayerPrefs.SetString("currentLanguage", EnglishCode);
                    break;
                case Turkish:
                    _leanLocalization.SetCurrentLanguage(TurkishCode);
                    PlayerPrefs.SetString("currentLanguage", TurkishCode);
                    break;
                case Russian:
                    _leanLocalization.SetCurrentLanguage(RussianCode);
                    PlayerPrefs.SetString("currentLanguage", RussianCode);
                    break;
            }
        }
    }
}
