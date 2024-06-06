using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _useButton;

    private Weapon _weapon;

    public Weapon Weapon => _weapon;

    public PlayersWeapon PlayersWeapon { get; private set; }

    public event UnityAction<Weapon, WeaponView> SellButtonClick;

    private void Start()
    {
        if (_weapon.WeaponState.IsBuying == true)
        {
            TryLockItem();
        }
    }

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

    public void GetLinkPlayer(PlayersWeapon playersWeapon)
    {
        PlayersWeapon = playersWeapon;
    }

    public void Render(Weapon weapon)
    {
        _weapon = weapon;
        _label.text = weapon.Label;
        _price.text = weapon.Price.ToString();
        _icon.sprite = weapon.Icon;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_weapon, this);
    }

    private void TryLockItem()
    {
        if (_weapon.WeaponState.IsBuying)
        {
            _buyButton.SetActive(false);
            _useButton.SetActive(true);
        }
    }
}
