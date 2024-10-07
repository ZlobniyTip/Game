using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AliveNeutral : MonoBehaviour
{
    [SerializeField] private List<Neutral> _neutrals;

    public event UnityAction DeathNeutrals;

    private void Start()
    {
        if (_neutrals.Count == 0)
            return;

        foreach (var neutral in _neutrals)
        {
            neutral.Died += KillNeutral;
        }
    }

    private void OnDisable()
    {
        if (_neutrals.Count == 0)
            return;

        foreach (var neutral in _neutrals)
        {
            neutral.Died -= KillNeutral;
        }
    }

    private void KillNeutral(Neutral neutral)
    {
        DeathNeutrals?.Invoke();
    }
}
