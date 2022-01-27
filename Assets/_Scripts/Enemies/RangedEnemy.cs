using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    float projectileSpeed;

    public override void Attack()
    {
        ProjectileManager temp = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<ProjectileManager>();
        temp.SetDamage(damage);
        temp.SetType(DamageTypes.Physical);
        temp.Move(Vector2.left, projectileSpeed);
    }
}
