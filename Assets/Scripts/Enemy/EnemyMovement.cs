using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterAnimation))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _delayBetweenMove;
    [SerializeField] private float _setDelay;

    private CharacterAnimation _characterAnimation;
    private Coroutine _patrolling;
    private Vector3 _turningAngle = new Vector3(0, 180, 0);

    private int _currentPoint;
    private float _turningSpeed = 1;

    public float CurrentSpeed => _speed;
    public bool ShouldTurnArround { get; private set; }

    private void Start()
    {
        _characterAnimation = GetComponent<CharacterAnimation>();
        _patrolling = StartCoroutine(Patrolling());
    }

    private IEnumerator Patrolling()
    {
        var DelayBetweenMove = new WaitForSeconds(_delayBetweenMove);

        while (true)
        {
            Transform target = _points[_currentPoint];

            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            if (transform.position == target.position)
            {
                _currentPoint++;

                _characterAnimation.TurnAround();
                transform.DORotate(_turningAngle, _turningSpeed, RotateMode.LocalAxisAdd).SetDelay(_setDelay);

                yield return DelayBetweenMove;

                if (_currentPoint >= _points.Length)
                {
                    _currentPoint = 0;
                }
            }

            yield return null;
        }
    }
}