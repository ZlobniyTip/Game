using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _index;

    [NonSerialized] private SkillState _state = null;

    public bool Used { get; private set; } = false;

    public string Label => _label;
    public string Description => _description;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public ItemState State => _state ??= new SkillState(ItemStatus.NotPurchased);

    public virtual void UseSkill(Weapon weapon)
    {
    }

    public void Init(ItemState skillState)
    {
        if (skillState == null)
            return;

        State.SetStatus(skillState.Status);
    }

    protected void LockSkill()
    {
        Used = true;
    }
}
