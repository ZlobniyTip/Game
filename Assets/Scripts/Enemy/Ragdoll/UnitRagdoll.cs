using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]
public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private UnitRagdollBone[] _bones;
    [SerializeField] private float _hitForce;

    private Animator _animator;
    private EnemyMovement _enemyMovement;
    private Creature _enemy;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Creature>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _bones.Length; i++)
        {
            _bones[i].GetHit += OnTakeHit;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _bones.Length; i++)
        {
            _bones[i].GetHit -= OnTakeHit;
        }
    }

    private void OnTakeHit(UnitRagdollBone damagedBone, Vector3 direction)
    {
        if (_enemyMovement == true)
        {
            _enemyMovement.enabled = false;
        }

        if (_animator.enabled == true)
        {
            _animator.enabled = false;
        }

        damagedBone.ApplyForce(direction * _hitForce);
        StartCoroutine(_enemy.Die());
    }
}