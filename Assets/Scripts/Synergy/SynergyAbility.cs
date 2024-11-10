using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SynergyAbility : MonoBehaviour
{
    public UnitBehaviour unitBehaviour;
    public abstract void Use();

    private void Start()
    {
        unitBehaviour = GetComponent<UnitBehaviour>();
        Use();
    }
}
