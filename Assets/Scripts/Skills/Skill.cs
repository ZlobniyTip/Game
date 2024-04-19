using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private SkillState _skillState;

    public bool Used { get; private set; } = false;

    public bool IsBuyed => _isBuyed;
    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public SkillState SkillState => _skillState;

    public virtual void UseSkill(Weapon weapon)
    {
    }

    protected void LockSkill()
    {
        Used = true;
    }

    public void ResetSkill()
    {
        _isBuyed = false;
    }

    public void Buy()
    {
        _isBuyed = true;
    }
}
