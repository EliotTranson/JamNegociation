using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBehaviour : UnitBehaviour
{
    //[Header("Mage settings")] 

    public override void FindTarget()
    {
        allEnemies.Clear();
        var AllEntities = FindObjectsOfType<UnitBehaviour>();
        
        
        foreach (var entity in AllEntities)
        {
            if (entity.GetComponent<UnitBehaviour>().team == team && entity.gameObject != gameObject)
            {
                allEnemies.Add(entity.gameObject);
            }
        }

        target = GetClosestEnemy();
    }
    
}
