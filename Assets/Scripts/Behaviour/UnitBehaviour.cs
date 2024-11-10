using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBehaviour : MonoBehaviour
{
    [Header("Stats")] 
    public float attackDamage;
    public float attackRange;
    
    [Space]
    public float hp;
    public float attackSpeed;
    public float movementSpeed = 10;
    public float testSpeed = 100;
    
    [Space]
    public bool team;
    private bool canAttack;
    private Animator anim;

    [Header("BehaviourNeeded")] 
    public GameObject target;
    public List<GameObject> allEnemies = new List<GameObject>();
    /*[HideInInspector]*/ public bool canFight;

    private Rigidbody rb;

    public MeshRenderer bodyMesh;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.speed = attackSpeed;
        canFight = false;
    }

    private void Update()
    {
        //death
        if (hp <= 0)
        {
            Die();
        }
        
        if (!canFight) return;
        
        SetBehaviour();

        SetAnimation();
    }

    private void FixedUpdate()
    {
        if (!canFight) return;
        
        if (canAttack)
        {
            ActionTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    public virtual void FindTarget()
    {
        allEnemies.Clear();
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

    public GameObject GetClosestEnemy()
    {
        float distance;
        float nearestDistance = 10000;
        GameObject nearestObj = null;

        if (allEnemies.Count == 0)
            return null;
        
        foreach (var enemy in allEnemies)
        {
            //if (enemy == null) return null;
            
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
            canAttack = false;
            FindTarget();
        } 
        
        if (target != null && Vector3.Distance(transform.position, target.transform.position) >= attackRange)
        {
            canAttack = false;
        }
        else if (target != null)
        {
            canAttack = true;
        }

        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    public virtual void MoveToTarget()
    {
        if (target == null) return;
        
        rb.AddForce((target.transform.position - transform.position).normalized * testSpeed, ForceMode.Acceleration);

        if (rb.velocity.magnitude > movementSpeed)
        {
            rb.velocity = rb.velocity.normalized * movementSpeed;  // Limit the velocity to x units per second
        }
    }
    public virtual void ActionTarget()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 10 * Time.deltaTime);
        
        if (canAttack)
        {
            anim.SetTrigger("Attack");
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
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

    public virtual void Attack()
    {
        if (target == null) return;
        
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            target.GetComponent<UnitBehaviour>().TakeDamage(attackDamage);
        }
    }
    
    
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    private void SetAnimation()
    {
        anim.SetFloat("Speed", rb.velocity.magnitude);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
