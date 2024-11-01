using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class Neutral : Creature
    {
        public event UnityAction<Neutral> Died;

        public override IEnumerator Die()
        {
            Died?.Invoke(this);
            var delay = new WaitForSeconds(1);

            DeathEffects?.Invoke();

            yield return delay;

            Destroy(gameObject);
        }
    }
}