using System.Collections;
using UnityEngine;

public class Loss : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void DeclareLoss()
    {
        StartCoroutine(OpenLossScreen());
    }

    private IEnumerator OpenLossScreen()
    {
        yield return new WaitForSeconds(5);

        _panel.SetActive(true);
    }
}