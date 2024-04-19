using System;

[Serializable]
public struct WeaponState
{
    public bool IsBuying;

    public void Buy()
    {
        IsBuying = true;
    }
}