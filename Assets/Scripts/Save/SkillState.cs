using UnityEngine;
using System;

[Serializable]
public class SkillState
{
    public bool IsBuying;

    public void Buy()
    {
        IsBuying = true;
    }
}