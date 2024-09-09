using System;

[Serializable]
public class SkinState: ItemState
{
    public SkinState(ItemStatus status)
    {
        Status = status;
    }
}