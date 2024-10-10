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
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _equippedLabel;

    private Skin _skin;

    public SkinEditor SkinEditor { get; private set; }

    public Skin Skin => _skin;

    public event UnityAction<SkinView> PurchaseButtonPressed;
    public event UnityAction<SkinView> EquipButtonPressed;

    private void OnEnable()
    {
        _equipButton.onClick.AddListener(OnEquipButtonPressed);
        _purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
    }

    private void OnDisable()
    {
        _equipButton.onClick.RemoveListener(OnEquipButtonPressed);
        _purchaseButton.onClick.RemoveListener(OnPurchaseButtonPressed);
    }

    private void OnDestroy()
    {
        _skin.State.Changed -= OnSkinStateChanged;
    }

    public void Init(Skin skin)
    {
        _skin = skin;

        _skin.State.Changed += OnSkinStateChanged;

        UpdateView();
    }

    private void UpdateView()
    {
        _label.text = _skin.Label;
        _price.text = _skin.Price.ToString();
        _icon.sprite = _skin.Icon;

        switch (_skin.State.Status)
        {
            case ItemStatus.NotPurchased:
                ShowPurchaseButton();
                break;
            case ItemStatus.Purchased:
                ShowEquipButton();
                break;
            case ItemStatus.Equipped:
                ShowEquippedLabel();
                break;
        }
    }

    private void ShowPurchaseButton()
    {
        _purchaseButton.gameObject.SetActive(true);
        _equipButton.gameObject.SetActive(false);
        _equippedLabel.gameObject.SetActive(false);
    }

    private void ShowEquipButton()
    {
        _purchaseButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(true);
        _equippedLabel.gameObject.SetActive(false);
    }

    private void ShowEquippedLabel()
    {
        _purchaseButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);
        _equippedLabel.gameObject.SetActive(true);
    }

    private void OnSkinStateChanged()
    {
        UpdateView();
    }

    private void OnPurchaseButtonPressed()
    {
        PurchaseButtonPressed?.Invoke(this);
    }

    private void OnEquipButtonPressed()
    {
        EquipButtonPressed?.Invoke(this);
    }
}
