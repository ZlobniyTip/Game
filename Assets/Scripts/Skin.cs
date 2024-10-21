using System;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _index;

    [NonSerialized] private ItemState _state = null;

    public string Label => _label;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

    public void Init(ItemStatus skinState)
    {
        State.SetStatus(skinState);
    }
}