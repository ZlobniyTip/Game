using System;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _index;
    [SerializeField] private SkinState _skinState;

    [NonSerialized] private SkinState _state = null;

    public string Label => _label;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public SkinState SkinState => _skinState;
    public SkinState State => _state ??= new SkinState(SkinStatus.NotPurchased);

    public void Init(SkinState skinState)
    {
        _state.SetStatus(skinState.Status);
    }
}