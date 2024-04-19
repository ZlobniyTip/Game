using System.Collections.Generic;
using UnityEngine;

public class SkillBuying : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SkillView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _player.Skills.Count; i++)
        {
            AddItem(_player.Skills[i]);
        }
    }

    private void AddItem(Skill skill)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(skill);
    }

    private void OnSellButtonClick(Skill skill, SkillView view)
    {
        TrySellSkill(skill, view);
    }

    private void TrySellSkill(Skill skill, SkillView view)
    {
        if (skill.Price <= _player.Money)
        {
            skill.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}