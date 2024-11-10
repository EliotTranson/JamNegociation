using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance;
    private bool gameStarted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeLists();
    }

    private void Update()
    {
        if (gameStarted) return;
        
        if (lastRedUnits.Count == 0 && lastBlueUnits.Count == 0)
        {
            Debug.Log("Start");
            foreach (var uBehaviour in FindObjectsOfType<UnitBehaviour>())
            {
                uBehaviour.canFight = true;
            }

            gameStarted = true;
        }
    }

    [Header("Blue Team")] 
    public List<GameObject> blueUnits;
    private List<GameObject> lastBlueUnits;
    [SerializeField] public Material blueBodyMat;
    
    [Header("Red Team")] 
    public List<GameObject> redUnits;
    private List<GameObject> lastRedUnits;
    [SerializeField] public Material redBodyMat;
    
    public void SummonUnit(Vector3 position, bool team)
    {
        GameObject unit = null;
        if (team && lastRedUnits.Count > 0)
        {
            unit = lastRedUnits[0];
        }
        else if(lastBlueUnits.Count > 0)
        {
            unit = lastBlueUnits[0];
        }
        else return;
        
        GameObject unitInstance = Instantiate(unit, position, Quaternion.identity);
        
        Material unitMaterial = team ? redBodyMat : blueBodyMat;
        unitInstance.GetComponent<UnitBehaviour>().bodyMesh.material = new Material(unitMaterial);
        unitInstance.GetComponent<UnitBehaviour>().team = team;
        
        if (team)
        {
            lastRedUnits.RemoveAt(0);
        }
        else lastBlueUnits.RemoveAt(0);
    }

    private void InitializeLists()
    {
        lastBlueUnits = blueUnits.ToList();
        lastRedUnits = redUnits.ToList();
    }
}
