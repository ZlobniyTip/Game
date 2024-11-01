using System.Collections;
using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemySoldier : Creature
    {
        [SerializeField] private ProgressBar _progressBar;

        public override IEnumerator Die()
        {
            var delay = new WaitForSeconds(1);

            DeathEffects?.Invoke();
            _progressBar.DeleteEnemy(this);

            yield return delay;

            Destroy(gameObject);
        }
    }
}