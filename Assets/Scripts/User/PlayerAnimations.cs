using UnityEngine;

namespace User
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Throw()
        {
            _animator.SetTrigger(Params.Throw);
        }

        public void Swing()
        {
            _animator.SetTrigger(Params.Swing);
        }
    }

    public class Params
    {
        public static readonly int Throw = Animator.StringToHash(nameof(Throw));
        public static readonly int Swing = Animator.StringToHash(nameof(Swing));
    }
}