using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _speedRotation;

    private Player _player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out UnitRagdollBone bone))
        {
            bone.TakeHit(transform.forward);
            _player.GetReward(bone.Reward);
        }

        if (collision.gameObject.TryGetComponent(out ExplosiveBarrel explosiveBarrel))
        {
            explosiveBarrel.Explode();
        }
    }

    public void Die()
    {
        StopCoroutine(Spin());
        Destroy(gameObject);
    }

    public IEnumerator Spin()
    {
        float timer = 0;
        float torsionTime = 2.5f;

        while (timer < torsionTime)
        {
            timer += Time.deltaTime;

            if (this != null)
            {
                transform.Rotate(0, 0, _speedRotation * Time.deltaTime);
            }

            yield return null;
        }
    }

    public void GetPlayerLink(Player player)
    {
        _player = player;
    }
}