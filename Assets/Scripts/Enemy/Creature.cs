using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField] public UnityEvent DeathEffects;

        public virtual IEnumerator Die()
        {
            yield return null;
        }
    }
}