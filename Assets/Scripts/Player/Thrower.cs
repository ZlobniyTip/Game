using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private PlayerUseSkills _playerUseSkills;
    [SerializeField] private Teleportation _teleportation;
    [SerializeField] private GameObject _touchLight;
    [SerializeField] private FollowCam _followCam;
    [SerializeField] private Ricochet _ricochet;
    [SerializeField] private Player _player;

    private Rigidbody _rbCurrentWeapon;
    private Weapon _weapon;
    private int _velocityMult = 12;

    public Weapon CurrentWeapon { get; private set; }
    public bool AimingMode { get; private set; }

    private void Awake()
    {
        _touchLight.SetActive(false);

        if (PlayerPrefs.HasKey("throwForce"))
        {
            _velocityMult = PlayerPrefs.GetInt("throwForce");
        }
    }

    private void OnMouseEnter()
    {
        _touchLight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _touchLight.SetActive(false);
    }

    private void OnMouseDown()
    {
        AimingMode = true;
        CurrentWeapon = Instantiate(_weapon, transform.position, _weapon.transform.rotation);
        CurrentWeapon.GetLinks(_player, _ricochet);
        _playerUseSkills.GetLinkWeapon(CurrentWeapon);
        _rbCurrentWeapon = CurrentWeapon.GetComponentInChildren<Rigidbody>();
        _rbCurrentWeapon.isKinematic = true;
    }

    private void Update()
    {
        if (!AimingMode)
            return;

        Vector3 mouseDelta = CalculateDirectionThrow();

        if (Input.GetMouseButtonUp(0))
        {
            Throw(mouseDelta);
        }
    }

    public void AddThrowForce(int force)
    {
        _velocityMult += force;
        PlayerPrefs.SetInt("throwForce", _velocityMult);
    }

    public void GiveWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void Throw(Vector3 mouseDelta)
    {
        _ricochet.ResetSkill();
        CurrentWeapon.transform.parent = null;
        AimingMode = false;
        _rbCurrentWeapon.isKinematic = false;
        _rbCurrentWeapon.velocity = mouseDelta * _velocityMult;
        _followCam.GetLinkToObservedObject(CurrentWeapon.transform);
        _teleportation.GetLinkCurrentWeapon(CurrentWeapon);
        CurrentWeapon = null;
        _player.ReduceNumThrows();
    }

    private Vector3 CalculateDirectionThrow()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - transform.position;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        CurrentWeapon.transform.position = mouseDelta;

        Vector3 weaponPosition = transform.position + mouseDelta;
        CurrentWeapon.transform.position = weaponPosition;

        return mouseDelta;
    }
}
