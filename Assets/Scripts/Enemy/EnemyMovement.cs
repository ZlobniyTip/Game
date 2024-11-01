using Animation;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _speed;
        [SerializeField] private float _delayBetweenMove;
        [SerializeField] private float _setDelay;

        private CharacterAnimation _characterAnimation;
        private Vector3 _turningAngle = new (0, 180, 0);

        private readonly float _turningSpeed = 1;
        private int _currentPoint;

        public float CurrentSpeed => _speed;

        private void Start()
        {
            _characterAnimation = GetComponent<CharacterAnimation>();
            StartCoroutine(Patrolling());
        }

        private IEnumerator Patrolling()
        {
            var delayBetweenMove = new WaitForSeconds(_delayBetweenMove);

            while (true)
            {
                Transform target = _points[_currentPoint];

                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

                if (transform.position == target.position)
                {
                    _currentPoint++;

                    _characterAnimation.TurnAround();
                    transform.DORotate(_turningAngle, _turningSpeed, RotateMode.LocalAxisAdd).SetDelay(_setDelay);

                    yield return delayBetweenMove;

                    if (_currentPoint >= _points.Length)
                    {
                        _currentPoint = 0;
                    }
                }

                yield return null;
            }
        }
    }
}