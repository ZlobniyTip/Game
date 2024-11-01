using Save;
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public List<ItemStatus> weaponStates = new()
        {
            ItemStatus.Equipped,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased
        };

        public List<ItemStatus> skinStates = new()
        {
            ItemStatus.Equipped,
            ItemStatus.Purchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased
        };

        public List<ItemStatus> skillStates = new()
        {
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased,
            ItemStatus.NotPurchased
        };

        public float velocityMult = 15;
        public int playerMoney = 0;
        public int maxNumberThrows = 5;
        public int score = 0;
    }
}