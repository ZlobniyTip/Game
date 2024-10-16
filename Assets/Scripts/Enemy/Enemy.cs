using System.Collections;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private ProgressBar _progressBar;

    public override IEnumerator Die()
    {
        var delay = new WaitForSeconds(1);

        _deathEffects?.Invoke();
        _progressBar.DeleteEnemy(this);

        yield return delay;

        Destroy(gameObject);
    }
}
