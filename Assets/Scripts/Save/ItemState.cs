using System;
using UnityEngine;

[Serializable]
public class ItemState
{
    public ItemStatus Status;

    public event Action Changed;

    public void SetStatus(ItemStatus status)
    {
        Status = status;
        Changed?.Invoke();
    }

    public ItemState(ItemStatus status)
    {
        Status = status;
    }
}