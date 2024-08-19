using System.Collections.Generic;
using System;

[Serializable]
public class GameCoreStruct
{
    public List<WeaponState> weaponStates;
    public List<SkinState> skinStates;
    public List<SkillState> skillStates;
    public int playerMoney;
    public int maxNumberThrows;
    public float velocityMult;
}