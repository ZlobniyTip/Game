using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private SkillState _skillState;

    public bool Used { get; private set; } = false;

    public string Label => _label;
    public string Description => _description;
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
}
