using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : Bar
{
    [SerializeField] private WinScreen _winCondition;
    [SerializeField] private List<Enemy> _enemies;

    private Coroutine _win;

    public int EnemyCount { get; private set; }

    public int CurrentEnemyCount => _enemies.Count;

    public event UnityAction<int, int> EnemyCountChange;

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
        if (_win != null)
        {
            StopCoroutine(_win);
        }
        EnemyCountChange -= OnValueChanged;

    }

    public void DeleteEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        EnemyCountChange?.Invoke(CurrentEnemyCount, EnemyCount);

        if (_enemies.Count == 0)
        {
            _win = StartCoroutine(Win());
        }
    }

    private IEnumerator Win()
    {
        var delay = new WaitForSeconds(1.5f);

        yield return delay;

        _winCondition.OpenWinScreen();
    }
}