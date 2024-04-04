using UnityEngine;

public class Ricochet : Skill
{
    [SerializeField] private float _ricochetForce;

    public void CalculateRicochet(Rigidbody weapon, Wall wall)
    {
        if (IsBuyed)
        {
            float angle = Random.Range(10, 50);
            Vector3 direction = wall.transform.right * angle;
            weapon.AddForce(direction * _ricochetForce);

            LockSkill();
        }
    }
}
