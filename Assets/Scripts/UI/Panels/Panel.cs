using UnityEngine;

namespace UI
{
    public class Panel : MonoBehaviour
    {
        public void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void ClosePanel(GameObject panel)
        {
            panel.SetActive(false);
        }
    }
}