using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : Bar
{
    [SerializeField] private WinScreen _winCondition;
    [SerializeField] private List<Enemy> _enemies;

    public int EnemyCount { get; private set; }

    public int CurrentEnemyCount => _enemies.Count;

    public event UnityAction<int, int> EnemyCountChange;
    public event UnityAction Win;

    private void Start()
    {
        EnemyCount = _enemies.Count;
        EnemyCountChange?.Invoke(CurrentEnemyCount, EnemyCount);
    }

    private void OnEnable()
    {
        EnemyCountChange += OnValueChanged;
    }

    private void OnDisable()
    {
        EnemyCountChange -= OnValueChanged;
    }

    public void DeleteEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        EnemyCountChange?.Invoke(CurrentEnemyCount, EnemyCount);

        if (_enemies.Count == 0)
        {
            Win?.Invoke();
        }
    }
}