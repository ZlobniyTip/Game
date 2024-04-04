using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent _deathEffects;
    [SerializeField] private ProgressBar _progressBar;

    private void Start()
    {
        _progressBar.AddEnemy(this);
    }

    public IEnumerator Die()
    {
        var delay = new WaitForSeconds(1);

        _deathEffects?.Invoke();
        _progressBar.DeleteEnemy(this);

        yield return delay;

        Destroy(gameObject);
    }
}
