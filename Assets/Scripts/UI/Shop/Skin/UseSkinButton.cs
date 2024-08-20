using UnityEngine;
using UnityEngine.Events;

public class UseSkinButton : MonoBehaviour
{
    [SerializeField] private SkinView _skinView;

    public event UnityAction ChangeSkin;

    public void UseSkin()
    {
        _skinView.SkinEditor.ChangeSkin(_skinView.Skin);
        ChangeSkin?.Invoke();
    }
}