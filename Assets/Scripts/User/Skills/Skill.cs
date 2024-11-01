using Save;
using UnityEngine;

namespace User
{
    public abstract class Skill : Product
    {
        [SerializeField] private string _description;

        public bool Used { get; private set; } = false;

        public virtual void UseSkill(Weapon weapon)
        {
            LockSkill();
        }

        public override void Init(ItemStatus state)
        {
            State.SetStatus(state);
        }

        protected void LockSkill()
        {
            Used = true;
        }
    }
}