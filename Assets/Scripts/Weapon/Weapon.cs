using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private WeaponState _weaponState;
    [SerializeField] private int _index;

    private Rigidbody _rigidbody;

    public string Label => _label;
    public int Price => _price;
    public int Index => _index;
    public Sprite Icon => _icon;
    public WeaponState WeaponState => _weaponState;

    public Player Player { get; private set; }
    public Ricochet Ricochet { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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

        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            if (Ricochet.Used == false)
            {
                Ricochet.CalculateRicochet(_rigidbody, wall);
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void GetLinks(Player player, Ricochet ricochet)
    {
        Player = player;
        Ricochet = ricochet;
    }
}