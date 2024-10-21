using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _equippedLabel;

    private Skill _skill;

    public Skill Skill => _skill;

    public event UnityAction<SkillView> PurchaseButtonPressed;
    public event UnityAction<SkillView> EquipButtonPressed;

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
        _skill.State.Changed -= OnSkillStateChanged;
    }

    public void Init(Skill skill)
    {
        _skill = skill;

        _skill.State.Changed += OnSkillStateChanged;

        UpdateView();
    }

    public void UpdateView()
    {
        _label.text = LeanLocalization.GetTranslationText(_skill.Label);
        _description.text = LeanLocalization.GetTranslationText(_skill.Description);
        _price.text = _skill.Price.ToString();
        _icon.sprite = _skill.Icon;

        switch (_skill.State.Status)
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

    private void OnSkillStateChanged()
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
