using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviour : UnitBehaviour
{
    [Header("Archer settings")] 
    [SerializeField] private GameObject projectile;
    public override void Attack()
    {
        if (target == null) return;
        
        GameObject arrow = Instantiate(projectile, transform.position, Quaternion.identity);
        arrow.GetComponent<ProjectileMovement>().target = target;
        arrow.GetComponent<ProjectileMovement>().damage = attackDamage;
    }
}
