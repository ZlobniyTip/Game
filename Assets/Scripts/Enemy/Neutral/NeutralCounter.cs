using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class NeutralCounter : MonoBehaviour
    {
        [SerializeField] private List<Neutral> _neutrals;

        public event UnityAction MurderedNeutral;

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
            MurderedNeutral?.Invoke();
        }
    }
}