using Save;

namespace User
{
    public class Skin : Product
    {
        public override void Init(ItemStatus state)
        {
            State.SetStatus(state);
        }
    }
}