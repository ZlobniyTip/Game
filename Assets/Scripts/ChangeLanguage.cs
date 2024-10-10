using UnityEngine;
using UnityEngine.UI;
using YG.Example;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private Button _rs;
    [SerializeField] private Button _tr;
    [SerializeField] private Button _en;
    [SerializeField] private LanguageExample _languageExample;

    private string _rus = "ru";
    private string _tur = "tr";

    private void OnEnable()
    {
        _rs.onClick.AddListener(SetRusLanguage);
        _tr.onClick.AddListener(SetTrLanguage);
        _en.onClick.AddListener(SetEngLanguage);
    }

    private void OnDisable()
    {
        _rs.onClick.RemoveListener(SetRusLanguage);
        _tr.onClick.RemoveListener(SetTrLanguage);
        _en.onClick.RemoveListener(SetEngLanguage);
    }

    private void SetRusLanguage()
    {
        _languageExample.SwitchLanguage(_rus);
    }

    private void SetTrLanguage()
    {
        _languageExample.SwitchLanguage(_tur);
    }

    private void SetEngLanguage()
    {
        _languageExample.SwitchLanguage(null);
    }
}