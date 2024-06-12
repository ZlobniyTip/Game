using System;

[Serializable]
public class SkillState
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