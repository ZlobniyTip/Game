using System;

[Serializable]
public class WeaponState
{
    public WeaponStatus Status;

    public event Action Changed;
    
    public WeaponState(WeaponStatus status)
    {
        Status = status;
    }

    public void SetStatus(WeaponStatus status)
    {
        Status = status;
        Changed?.Invoke();
    }
}