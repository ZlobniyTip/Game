using UnityEngine;

public class UseSkinButton : MonoBehaviour
{
    [SerializeField] private SkinView _skinView;

    public void UseSkin()
    {
        _skinView.SkinEditor.ChangeSkin(_skinView.Skin);
    }
}