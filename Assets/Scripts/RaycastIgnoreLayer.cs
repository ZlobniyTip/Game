using UnityEngine;

public class RaycastIgnoreLayer : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskOnlyPlayer;

    private void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray.origin, _ray.direction, Mathf.Infinity, _layerMaskOnlyPlayer));
    }
}