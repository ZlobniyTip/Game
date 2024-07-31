using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshCollider))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private WeaponState _weaponState;
    [SerializeField] private int _index;
    [SerializeField] private MeshCollider _meshCollider;

    public event UnityAction Destruction;

    private Thrower _thrower;

    public string Label => _label;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public WeaponState WeaponState => _weaponState;

    public Player Player { get; private set; }

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitRagdollBone bone))
        {
            bone.TakeHit(transform.forward);
            Player.GetReward(bone.Reward);
        }

        if (collision.gameObject.TryGetComponent(out ExplosiveBarrel explosiveBarrel))
        {
            explosiveBarrel.Explode();
        }
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        Destruction?.Invoke();
    }

    public void GetLinks(Player player)
    {
        Player = player;
    }

    public void SwitchingCollider(bool isIncluded)
    {
        _meshCollider.enabled = isIncluded;
    }
}