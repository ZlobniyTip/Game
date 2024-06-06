using UnityEngine;

public class ThrowDefault : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Thrower _thrower;

    private void Start()
    {
        ResetThrowCount();
    }

    public void ResetThrowCount()
    {
        _player.ResetThrowCount();
        _thrower.ResetThrowForce();
    }
}