using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _useButton;

    private Skill _skill;

    public event UnityAction<Skill, SkillView> SellButtonClick;

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

    public void Render(Skill skill)
    {
        _skill = skill;

        _label.text = LeanLocalization.GetTranslationText(skill.Label);
        _price.text = skill.Price.ToString();
        _icon.sprite = skill.Icon;

        if (_skill.SkillState.IsBuying)
        {
            TryLockItem();
        }
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_skill, this);
    }

    private void TryLockItem()
    {
        _sellButton.gameObject.SetActive(false);
        _useButton.SetActive(true);
    }
}
