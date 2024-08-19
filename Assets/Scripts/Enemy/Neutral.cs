using System.Collections;
using UnityEngine;

public class Neutral : Creature
{
    public override IEnumerator Die()
    {
        var delay = new WaitForSeconds(1);

        _deathEffects?.Invoke();

        yield return delay;

        Destroy(gameObject);
    }
}
