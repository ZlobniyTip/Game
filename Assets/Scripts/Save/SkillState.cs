using System;

[Serializable]
public class SkillState
{
    public SkillStatus Status;

    public event Action Changed;

    public SkillState(SkillStatus status)
    {
        Status = status;
    }

    public void SetStatus(SkillStatus status)
    {
        Status = status;
        Changed?.Invoke();
    }
}