using System.Collections.Generic;
using UnityEngine;

public class SkillBuying : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SkillView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<SkillView> _content = new List<SkillView>();

    private void OnEnable()
    {
        for (int i = 0; i < _player.Skills.Count; i++)
        {
            AddItem(_player.Skills[i]);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            _content[i].SellButtonClick -= OnSellButtonClick;
            Destroy(_content[i].gameObject);
        }

        _content.Clear();
    }

    private void AddItem(Skill skill)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(skill);
        _content.Add(view);
    }

    private void OnSellButtonClick(Skill skill, SkillView view)
    {
        Debug.Log("onSellButtonCLickSkin");
        TrySellSkill(skill, view);
    }

    private void TrySellSkill(Skill skill, SkillView view)
    {
        if (skill.Price <= _player.Money)
        {
            _player.BuySkill(skill);
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}