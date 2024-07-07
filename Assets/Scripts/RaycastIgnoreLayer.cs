using UnityEngine;

public class RaycastIgnoreLayer : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskOnlyPlayer;
    [SerializeField] private Thrower _thrower;

    private void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray.origin, _ray.direction, Mathf.Infinity, _layerMaskOnlyPlayer))
        {
            _thrower.ToutchLigthOn();

            if (Input.GetMouseButtonDown(0))
            {
                _thrower.MouseDown();
            }
        }
        else
        {
            _thrower.ToutchLigthOff();
        }
    }
}