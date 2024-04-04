using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkinView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Skin _skin;

    public event UnityAction<Skin, SkinView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    public void Render(Skin skin)
    {
        _skin = skin;
        _label.text = skin.Label;
        _price.text = skin.Price.ToString();
        _icon.sprite = skin.Icon;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_skin, this);
    }

    private void TryLockItem()
    {
        if (_skin.IsBuyed)
        {
            _sellButton.interactable = false;
        }
    }
}
