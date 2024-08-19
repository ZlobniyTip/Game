using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";
    private const string AnonymousName = "Anonymous";

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private GameObject _authorizePanel;
    [SerializeField] private GameObject _errorAuthorizedPanel;
    [SerializeField] private SaveState _saveState;

    public void OpenLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();
            Fill();
            _authorizePanel.SetActive(false);

        if (PlayerAccount.IsAuthorized == false)
            _authorizePanel.SetActive(true);

        _saveState.LoadFile();
    }

    public void Authorize()
    {
        PlayerAccount.Authorize(OpenLeaderboard, OpenAuthorizationWindow);

        if (PlayerAccount.IsAuthorized)
            _authorizePanel.SetActive(false);
    }

    public void SetPlayerScore(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Leaderboard.SetScore(LeaderboardName, score);
        });
    }

    public void Fill()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                {
                    name = AnonymousName;
                }

                _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
            }

            _leaderboardView.Constructleaderboard(_leaderboardPlayers);
        });
    }

    private void OpenAuthorizationWindow(string error)
    {
        _errorAuthorizedPanel.SetActive(true);
        _authorizePanel.SetActive(true);
    }
}