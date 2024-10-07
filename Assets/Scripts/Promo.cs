using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promo : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skins;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(ChangeSkins());

    }

    private IEnumerator ChangeSkins()
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            yield return new WaitForSeconds(1f);

            _skins[i].SetActive(false);
            _skins[i + 1].SetActive(true);
        }
    }
}
