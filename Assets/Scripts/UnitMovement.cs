using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject[] allEnemies;
    [SerializeField] private GameObject currentEnemy;
    [SerializeField] private float speed = 5;
    private float distance;

    private void Start()
    {
        currentEnemy = FindTarget();
    }

    private void Update()
    {
        if (currentEnemy != null && Vector2.Distance(transform.position, currentEnemy.transform.position) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentEnemy.transform.position, speed * Time.deltaTime);
            Debug.Log(Vector2.Distance(transform.position, currentEnemy.transform.position));
        }
    }

    private GameObject FindTarget()
    {
        GameObject nearestEnemy = null;
        allEnemies = GameObject.FindGameObjectsWithTag("Player");

        float nearestDistance = 10000;

        foreach (var enemy in allEnemies)
        {
            distance = Vector2.Distance(transform.position, enemy.transform.position);
            
            if (distance > 0.5f && distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }
}
