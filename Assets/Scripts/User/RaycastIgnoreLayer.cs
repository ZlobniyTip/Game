using UnityEngine;

namespace User
{
    public class RaycastIgnoreLayer : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskOnlyPlayer;
        [SerializeField] private Thrower _thrower;

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, Mathf.Infinity, _layerMaskOnlyPlayer))
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
}