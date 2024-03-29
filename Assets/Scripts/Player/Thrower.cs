using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private PlayerUseSkills _playerUseSkills;
    [SerializeField] private Teleportation _teleportation;
    [SerializeField] private GameObject _touchLight;
    [SerializeField] private FollowCam _followCam;
    [SerializeField] private float _velocityMult;
    [SerializeField] private Ricochet _ricochet;
    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;

    private Rigidbody _rbCurrentWeapon;

    public Weapon CurrentWeapon { get; private set; }
    public bool AimingMode { get; private set; }

    private void Awake()
    {
        _touchLight.SetActive(false);
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
        CurrentWeapon.StartSpin();
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

    private void Throw(Vector3 mouseDelta)
    {
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
