using System;

[Serializable]
public class WeaponState
{
    public bool IsBuying;
    public bool IsUsed;

    public void Buy()
    {
        IsBuying = true;
    }

    public void Used(bool isUsed)
    {
        IsUsed = isUsed;
    }

    public void LoadState(bool isBuying,bool isUsed)
    {
        IsBuying = isBuying;
        IsUsed = isUsed;
    }
}