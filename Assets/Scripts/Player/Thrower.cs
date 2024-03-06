using UnityEngine;

[RequireComponent(typeof(Ricochet))]
public class Thrower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Teleportation _teleportation;
    [SerializeField] private FollowCam _followCam;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private GameObject _touchLight;
    [SerializeField] private float _velocityMult;

    private Rigidbody _rbCurrentWeapon;
    private Ricochet _ricochet;

    public Weapon CurrentWeapon { get; private set; }
    public bool AimingMode { get; private set; }

    private void Awake()
    {
        _ricochet = GetComponent<Ricochet>();
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
        CurrentWeapon = Instantiate(_weapon, _launchPoint.transform);
        CurrentWeapon.GetLinks(_player,_ricochet);
        CurrentWeapon.transform.SetParent(_launchPoint.transform, worldPositionStays: false);
        CurrentWeapon.transform.position = _launchPoint.position;
        _rbCurrentWeapon = CurrentWeapon.GetComponentInChildren<Rigidbody>();
        _rbCurrentWeapon.isKinematic = true;
    }

    private void Update()
    {
        if (!AimingMode)
            return;

        Vector3 mouseDelta = CalculateDirectionThrow();

        Throw(mouseDelta);
    }

    private void Throw(Vector3 mouseDelta)
    {
        if (Input.GetMouseButtonUp(0))
        {
            //StartCoroutine(CurrentWeapon.Spin());
            CurrentWeapon.transform.parent = null;
            AimingMode = false;
            _rbCurrentWeapon.isKinematic = false;
            _rbCurrentWeapon.velocity = mouseDelta * _velocityMult;
            _followCam.GetLinkToObservedObject(CurrentWeapon.transform);
            _teleportation.GetLinkCurrentWeapon(CurrentWeapon);
            CurrentWeapon = null;
            _player.ReduceNumThrows();
        }
    }

    private Vector3 CalculateDirectionThrow()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - _launchPoint.position;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        CurrentWeapon.transform.position = mouseDelta;

        //Vector3 weaponPosition = _launchPoint.position + mouseDelta;
        //CurrentWeapon.transform.position = weaponPosition;

        return mouseDelta;
    }
}
