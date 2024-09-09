using System.Collections.Generic;
using System;

[Serializable]
public class GameCoreStruct
{
    public List<ItemState> weaponStates;
    public List<ItemState> skinStates;
    public List<ItemState> skillStates;
    public int playerMoney;
    public int maxNumberThrows;
    public int score;
    public float velocityMult;
}