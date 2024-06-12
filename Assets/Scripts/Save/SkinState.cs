using System;

[Serializable]
public class SkinState
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