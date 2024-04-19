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
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _useButton;

    private Skin _skin;
    private int _lockItem = 0;

    public SkinEditor SkinEditor { get; private set; }

    public Skin Skin => _skin;

    public event UnityAction<Skin, SkinView> SellButtonClick;

    private void Start()
    {
        _lockItem = PlayerPrefs.GetInt("LockItem");

        if (_lockItem == 1)
        {
            LockItem();
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

    public void GetLinkPlayer(SkinEditor skinEditor)
    {
        SkinEditor = skinEditor;
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
            _lockItem = 1;
            PlayerPrefs.SetInt("LockItem", _lockItem);

            LockItem();
        }
    }

    private void LockItem()
    {
        _buyButton.SetActive(false);
        _useButton.SetActive(true);
    }
}
