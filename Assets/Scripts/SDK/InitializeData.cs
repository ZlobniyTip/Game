using UnityEngine;
using YG;

namespace SDK
{
    public class InitializeData : MonoBehaviour
    {
        private void Start()
        {
            YandexGame.GameplayStart();
        }
    }
}