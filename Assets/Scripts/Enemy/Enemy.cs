using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent _deathEffects;

    public IEnumerator Die()
    {
        var delay = new WaitForSeconds(1);

        _deathEffects?.Invoke();

        yield return delay;

        Destroy(gameObject);
    }
}
