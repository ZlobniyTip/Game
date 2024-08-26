using System;

[Serializable]
public class SkinState
{
    public SkinStatus Status;

    public event Action Changed;

    public SkinState(SkinStatus status)
    {
        Status = status;
    }

    public void SetStatus(SkinStatus status)
    {
        Status = status;
        Changed?.Invoke();
    }
}