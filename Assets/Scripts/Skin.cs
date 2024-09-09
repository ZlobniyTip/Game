using System;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _index;

    [NonSerialized] private SkinState _state = null;

    public string Label => _label;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public ItemState State => _state ??= new SkinState(ItemStatus.NotPurchased);

    public void Init(ItemState skinState)
    {
        if (skinState == null)
            return;

        State.SetStatus(skinState.Status);
    }
}