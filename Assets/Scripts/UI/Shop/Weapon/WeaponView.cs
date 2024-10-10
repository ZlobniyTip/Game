using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Lean.Localization;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private GameObject _equippedLabel;

    private Weapon _weapon;

    public Weapon Weapon => _weapon;
    
    public event UnityAction<WeaponView> PurchaseButtonPressed;
    public event UnityAction<WeaponView> EquipButtonPressed;

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
        _weapon.State.Changed -= OnWeaponStateChanged;
    }

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
        
        _weapon.State.Changed += OnWeaponStateChanged;

        UpdateView();
    }

    private void UpdateView()
    {
        _label.text = _weapon.Label;
        _price.text = _weapon.Price.ToString();
        _icon.sprite = _weapon.Icon;
        
        switch (_weapon.State.Status)
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

    private void OnWeaponStateChanged()
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