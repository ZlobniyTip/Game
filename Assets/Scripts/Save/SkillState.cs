using UnityEngine;
using System;

[Serializable]
public struct SkillState
{
    public bool IsBuying;

    public void Buy()
    {
        IsBuying = true;
    }
}