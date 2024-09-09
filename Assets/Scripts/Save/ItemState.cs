using System;
using UnityEngine;

[Serializable]
public abstract class ItemState : MonoBehaviour
{
    public ItemStatus Status;

    public event Action Changed;

    public void SetStatus(ItemStatus status)
    {
        Status = status;
        Changed?.Invoke();
    }
}