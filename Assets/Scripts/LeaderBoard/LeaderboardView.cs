using System.Collections.Generic;
using UnityEngine;
using YG;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    private List<LeaderboardElement> _spawnedElements = new();

    public void Constructleaderboard(List<LBPlayerDataYG> leaderboardPlayers)
    {
        ClearLeaderboard();

        foreach (LBPlayerDataYG player in leaderboardPlayers)
        {
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);
            leaderboardElementInstance.Initialize(player.data.name, player.data.rank, player.data.score);

            _spawnedElements.Add(leaderboardElementInstance);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
        {
            Destroy(element);
        }

        _spawnedElements = new List<LeaderboardElement>();
    }
}
