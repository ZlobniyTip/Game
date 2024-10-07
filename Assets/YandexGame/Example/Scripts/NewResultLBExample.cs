using UnityEngine;

namespace YG.Example
{
    public class NewResultLBExample : MonoBehaviour
    {
        [SerializeField] private LeaderboardYG _leaderboardYG;

        public void NewScore(int score)
        {
            YandexGame.NewLeaderboardScores(_leaderboardYG.nameLB, score);
        }
    }
}