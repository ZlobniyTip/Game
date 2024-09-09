using System.Collections.Generic;
using UnityEngine;

public class AliveNeutral : MonoBehaviour
{
    [SerializeField] private List<Neutral> _neutrals;
    [SerializeField] private Loss _loss;

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

    public void KillNeutral(Neutral neutral)
    {
        _loss.DeclareLoss();
    }
}
