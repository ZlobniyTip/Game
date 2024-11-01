using Enemy;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyMovement))]
    public class CharacterAnimation : MonoBehaviour
    {
        private EnemyMovement _enemyMovement;
        private Animator _animator;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetupAnimation(_enemyMovement.CurrentSpeed);
        }

        public void TurnAround()
        {
            _animator.SetTrigger(Params.TurnAround);
        }

        private void SetupAnimation(float speed)
        {
            _animator.SetFloat(Params.Speed, Mathf.Abs(speed));
        }
    }

    public class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int TurnAround = Animator.StringToHash(nameof(TurnAround));
    }
}