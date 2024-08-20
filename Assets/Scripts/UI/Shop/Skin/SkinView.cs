using Lean.Localization;
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
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _usedButton;
    [SerializeField] private UseSkinButton _useSkinButton;

    private Skin _skin;

    public SkinEditor SkinEditor { get; private set; }

    public Skin Skin => _skin;

    public event UnityAction<Skin, SkinView> SellButtonClick;
    public event UnityAction<SkinView> UsedSkinView;
    public event UnityAction<SkinView> ChangeUsedSkin;

    private void OnEnable()
    {
        _useSkinButton.ChangeSkin += ChangeWeapon;
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(LockItem);
    }

    private void OnDisable()
    {
        _useSkinButton.ChangeSkin -= ChangeWeapon;
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(LockItem);
    }

    public void GetLinkPlayer(SkinEditor skinEditor)
    {
        SkinEditor = skinEditor;
    }

    public void Render(Skin skin)
    {
        _skin = skin;

        _label.text = LeanLocalization.GetTranslationText(skin.Label);
        _price.text = skin.Price.ToString();
        _icon.sprite = skin.Icon;

        if (_skin.SkinState.IsUsed)
        {
            ShowUsedButton();
            UsedSkinView?.Invoke(this);
            return;
        }
        else if (_skin.SkinState.IsBuying)
        {
            LockItem();
        }
        else if (_skin.Index == 0 || _skin.Index == 1)
        {
            LockItem();
        }
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_skin, this);
    }

    public void LockItem()
    {
        _sellButton.gameObject.SetActive(false);
        _useButton.gameObject.SetActive(true);
        _usedButton.gameObject.SetActive(false);
    }

    public void ShowUsedButton()
    {
        _sellButton.gameObject.SetActive(false);
        _useButton.gameObject.SetActive(false);
        _usedButton.gameObject.SetActive(true);
    }

    private void ChangeWeapon()
    {
        ChangeUsedSkin?.Invoke(this);
    }
}
