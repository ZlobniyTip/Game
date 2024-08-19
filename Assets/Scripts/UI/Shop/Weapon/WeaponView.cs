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
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _usedButton;
    [SerializeField] private UseWeaponButton _useWeaponButton;

    private Weapon _weapon;
    private string _defaultWeaponName = "SM_Wep_Kunai_01";

    public Weapon Weapon => _weapon;

    public PlayersWeapon PlayersWeapon { get; private set; }

    public event UnityAction<Weapon, WeaponView> SellButtonClick;
    public event UnityAction<WeaponView> UsedWeaponView;
    public event UnityAction<WeaponView> ChangeUsedWeapon;

    private void OnEnable()
    {
        _useWeaponButton.ChangeWeapon += ChangeWeapon;
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(LockItem);
    }

    private void OnDisable()
    {
        _useWeaponButton.ChangeWeapon -= ChangeWeapon;
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(LockItem);
    }

    public void GetLinkPlayer(PlayersWeapon playersWeapon)
    {
        PlayersWeapon = playersWeapon;
    }

    public void Render(Weapon weapon)
    {
        _weapon = weapon;

        _label.text = LeanLocalization.GetTranslationText(weapon.Label);
        _price.text = weapon.Price.ToString();
        _icon.sprite = weapon.Icon;

        if (_weapon.WeaponState.IsBuying)
        {
            LockItem();
        }

        if (_weapon.name == _defaultWeaponName)
        {
            LockItem();
        }

        if (_weapon.WeaponState.IsUsed)
        {
            ShowUsedButton();
            UsedWeaponView?.Invoke(this);
        }
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

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_weapon, this);
    }

    private void ChangeWeapon()
    {
        ChangeUsedWeapon?.Invoke(this);
    }
}