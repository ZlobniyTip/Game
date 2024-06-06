using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

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
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLocalization.SetCurrentLanguage(EnglishCode);
                    break;
                case Turkish:
                    _leanLocalization.SetCurrentLanguage(TurkishCode);
                    break;
                case Russian:
                    _leanLocalization.SetCurrentLanguage(RussianCode);
                    break;
            }
        }
    }
}