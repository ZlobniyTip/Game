using System;

[Serializable]
public struct SkinState
{
    public bool IsBuying;

    public void Buy()
    {
        IsBuying = true;
    }
}