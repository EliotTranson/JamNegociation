using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBehaviour : MonoBehaviour
{
    public float attackDamage;
    public float attackRange;
    public float hp;
    public float attackSpeed;
    public float movementSpeed = 10;

    public bool team;
    [HideInInspector] public bool canAttack;
    private Animator anim;

    [Header("BehaviourNeeded")] 
    public GameObject target;
    private List<GameObject> allEnemies = new List<GameObject>();

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetBehaviour();
    }

    public virtual void FindTarget()
    {
        var AllEntities = FindObjectsOfType<UnitBehaviour>();
        
        
        foreach (var entity in AllEntities)
        {
            if (entity.GetComponent<UnitBehaviour>().team != team)
            {
                allEnemies.Add(entity.gameObject);
            }
        }

        target = GetClosestEnemy();
    }

    private GameObject GetClosestEnemy()
    {
        float distance;
        float nearestDistance = 10000;
        GameObject nearestObj = null;
        
        foreach (var enemy in allEnemies)
        {
            distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < nearestDistance)
            {
                nearestObj = enemy;
                nearestDistance = distance;
            }
        }

        return nearestObj;
    }

    private void SetBehaviour()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        transform.LookAt(target.transform);
        
        if (Vector3.Distance(transform.position, target.transform.position) >= attackRange)
        {
            canAttack = false;
            MoveToTarget();
        }
        else
        {
            canAttack = true;
            ActionTarget();
        }
    }

    public virtual void MoveToTarget()
    {
        
        rb.AddForce((target.transform.position - transform.position).normalized * 1f, ForceMode.Acceleration);

        if (rb.velocity.magnitude > movementSpeed)
        {
            rb.velocity = rb.velocity.normalized * movementSpeed;  // Limit the velocity to x units per second
        }
    }
    public virtual void ActionTarget()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 5 * Time.deltaTime);
        
        if (canAttack)
        {
            anim.SetTrigger("Attack");
        }
    }
    
    

    private void ChangeAttackState(int state)
    {
        if (state == 0)
        {
            canAttack = false;
        }

        if (state == 1)
        {
            canAttack = true;
        }
    }

    private void Attack()
    {
        ;
    }
    
    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
    }

    public ScriptableObject SynergyScriptable;

    public virtual void OnSpawned()
    {
        
    }

    public virtual void OnKill()
    {
        
    }

    public virtual void OnFight()
    {
        
    }

    public virtual void EveryXSeconds()
    {
        
    }
}
