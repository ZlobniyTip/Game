using System.Collections.Generic;
using UnityEngine;

public class SkillShop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SkillView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<SkillView> _content = new List<SkillView>();

    private void OnEnable()
    {
        foreach (var skill in _player.Skills)
            AddSkillView(skill);
    }

    private void OnDisable()
    {
        foreach (var skillView in _content)
        {
            skillView.PurchaseButtonPressed -= OnPurchaseButtonPressed;
            skillView.EquipButtonPressed -= OnEquipButtonPressed;
            Destroy(skillView.gameObject);
        }

        _content.Clear();
    }

    private void AddSkillView(Skill skill)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(skill);
        view.PurchaseButtonPressed += OnPurchaseButtonPressed;
        view.EquipButtonPressed += OnEquipButtonPressed;
        _content.Add(view);
    }

    private void OnPurchaseButtonPressed(SkillView view)
    {
        _player.TryPurchaseSkill(view.Skill);
    }

    private void OnEquipButtonPressed(SkillView view)
    {
        _player.EquipSkill(view.Skill);
    }
}