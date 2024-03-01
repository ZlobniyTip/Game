using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _maxNumberThrows = 6;
    private int _money;

    public event UnityAction<int> MoneyChange;
    public event UnityAction<int,int> ThrowsChange;

    public int RemainingNumThrows { get; private set; }

    private void Start()
    {
        RemainingNumThrows = _maxNumberThrows;
        ThrowsChange?.Invoke(RemainingNumThrows, _maxNumberThrows);
    }

    public void ReduceNumThrows()
    {
        RemainingNumThrows--;
        ThrowsChange?.Invoke(RemainingNumThrows,_maxNumberThrows);
    }

    public void GetReward(int reward)
    {
        _money += reward;
        MoneyChange?.Invoke(_money);
    }
}
