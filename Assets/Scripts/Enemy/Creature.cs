using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected UnityEvent _deathEffects;

    public virtual IEnumerator Die()
    {
        yield return null;
    }
}
