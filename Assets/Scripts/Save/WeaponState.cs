using System;

[Serializable]
public class WeaponState : ItemState
{
    public WeaponState(ItemStatus status)
    {
        Status = status;
    }
}