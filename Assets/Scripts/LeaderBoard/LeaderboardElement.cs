using Lean.Localization;
using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    private const string _anonim = "Anonymous";

    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(string name, string rank, string score)
    {
        if (name == null)
        {
            name = LeanLocalization.GetTranslationText(_anonim);
        }
        else
        {
            _playerName.text = name;
        }

        _playerRank.text = rank;
        _playerScore.text = score;
    }
}
