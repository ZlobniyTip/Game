using System;

[Serializable]
public class WeaponState
{
    public bool IsBuying;

    public void Buy()
    {
        IsBuying = true;
    }

    public void LoadState(bool isBuying)
    {
        IsBuying = isBuying;
    }
}