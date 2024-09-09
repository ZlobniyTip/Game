using System;

[Serializable]
public class SkillState: ItemState
{
    public SkillState(ItemStatus status)
    {
        Status = status;
    }
}