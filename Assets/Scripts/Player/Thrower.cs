using UnityEngine;
using UnityEngine.Events;

public class Thrower : MonoBehaviour
{
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerUseSkills _playerUseSkills;
    [SerializeField] private ThrowerRaycast _throwerRaycast;
    [SerializeField] private Teleportation _teleportation;
    [SerializeField] private GameObject _touchLight;
    [SerializeField] private FollowCam _followCam;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _target;
    [SerializeField] private Ricochet _ricochet;

    private Rigidbody _rbCurrentWeapon;
    private Weapon _weapon;
    private float _velocityMult = 12;
    private float _velocityMultDefault = 12;

    public GameObject CurrentTarget { get; private set; }
    public Weapon CurrentWeapon { get; private set; }
    public bool AimingMode { get; private set; }
    public bool WeaponsFlight { get; private set; }

    private void Awake()
    {
        _touchLight.SetActive(false);

        if (PlayerPrefs.HasKey("throwForce"))
        {
            _velocityMult = PlayerPrefs.GetFloat("throwForce");
        }
    }

    private void Update()
    {
        if (!AimingMode)
            return;

        Vector3 mouseDelta = CalculateDirectionThrow();

        if (Input.GetMouseButtonUp(0))
        {
            _throwerRaycast.enabled = false;
            Throw(mouseDelta);
            _playerAnimations.Throw();
            WeaponsFlight = true;
        }
    }

    public void ToutchLigthOn()
    {
        _touchLight.SetActive(true);
    }

    public void ToutchLigthOff()
    {
        _touchLight.SetActive(false);
    }

    public void MouseDown()
    {
        _playerAnimations.Swing();
        AimingMode = true;
        _throwerRaycast.enabled = true;
        CurrentTarget = Instantiate(_target, transform.position, Quaternion.identity);
        CurrentWeapon = Instantiate(_weapon, CurrentTarget.transform.position, _weapon.transform.rotation);
        CurrentWeapon.transform.parent = CurrentTarget.transform;
        CurrentWeapon.GetLinks(_player);
        _ricochet.GetLinkCurrentWeapon(CurrentWeapon);
        _throwerRaycast.GetLinkWeapon(CurrentWeapon);
        _playerUseSkills.GetLinkWeapon(CurrentWeapon);
        _rbCurrentWeapon = CurrentWeapon.GetComponentInChildren<Rigidbody>();
        _rbCurrentWeapon.isKinematic = true;
        CurrentWeapon.SwitchingCollider(false);
    }

    public void ResetThrowForce()
    {
        _velocityMult = _velocityMultDefault;
        PlayerPrefs.SetFloat("throwForce", _velocityMult);
    }

    public void ResetFlightStatusWeapon()
    {
        WeaponsFlight = false;
    }

    public void AddThrowForce(float force)
    {
        _velocityMult += force;
        PlayerPrefs.SetFloat("throwForce", _velocityMult);
    }

    public void GiveWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void Throw(Vector3 mouseDelta)
    {
        CurrentWeapon.transform.parent = null;
        AimingMode = false;
        _rbCurrentWeapon.isKinematic = false;
        _rbCurrentWeapon.velocity = mouseDelta * _velocityMult;
        _followCam.GetLinkToObservedObject(CurrentWeapon.transform);
        _teleportation.GetLinkCurrentWeapon(CurrentWeapon);
        CurrentWeapon.SwitchingCollider(true);
        //CurrentWeapon = null;
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

        CurrentTarget.transform.position = mouseDelta;

        Vector3 weaponPosition = transform.position + mouseDelta;
        CurrentTarget.transform.position = weaponPosition;

        return mouseDelta;
    }
}