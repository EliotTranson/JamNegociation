using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviour : UnitBehaviour
{
    [Header("Archer settings")] 
    [SerializeField] private GameObject projectile;
    public override void Attack()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileMovement>().target = target.transform;
        projectile.GetComponent<ProjectileMovement>().damage = attackDamage;
    }
}
