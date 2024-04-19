using System.Collections.Generic;
using System;

[Serializable]
public struct GameCoreStruct
{
    public List<WeaponState> weaponStates;
    public List<SkinState> skinStates;
    public List<SkillState> skillStates;
}