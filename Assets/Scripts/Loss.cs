using System.Collections;
using UnityEngine;

public class Loss : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TestFocus _testFocus;

    public void DeclareLoss()
    {
        _testFocus.enabled = false;
        StartCoroutine(OpenLossScreen());
    }

    private IEnumerator OpenLossScreen()
    {
        yield return new WaitForSeconds(2);

        _panel.SetActive(true);

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }
}