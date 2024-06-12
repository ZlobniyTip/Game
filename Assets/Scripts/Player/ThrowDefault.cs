using UnityEngine;

public class ThrowDefault : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Thrower _thrower;

    private void Awake()
    {
        ResetThrowCount();
    }

    public void ResetThrowCount()
    {
        _player.ResetThrowCount();
        _thrower.ResetThrowForce();
    }
}