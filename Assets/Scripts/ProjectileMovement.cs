using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float projectileSpeed = 10;

    [HideInInspector] public float damage;

    private void Update()
    {
        if (target == null) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            target.GetComponent<UnitBehaviour>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
}