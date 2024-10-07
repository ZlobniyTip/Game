using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(string name, string rank, string score)
    {
        _playerName.text = name;
        _playerRank.text = rank;
        _playerScore.text = score;
    }
}
