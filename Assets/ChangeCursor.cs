using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private GameObject Cursor1, Cursor2;
    private PlayerInputManager pIM;

    private void Start()
    {
        pIM = GetComponent<PlayerInputManager>();
    }

    public void ChangePlayerPrefab()
    {
        pIM.playerPrefab = Cursor2;
    }
}
