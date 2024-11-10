using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField] private float cursorSpeed = 500;
    private Vector2 moveVector;
    [SerializeField] private bool team;
    [SerializeField] private RectTransform rectTransform;
    void Start()
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    private void FixedUpdate()
    {
        transform.position += (new Vector3(moveVector.x, moveVector.y, 0) * cursorSpeed) * Time.fixedDeltaTime;
    }

    private void RaycastInstancingUnit()
    {
        var cam = Camera.main;
        /*RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit);*/
        
        Ray ray = cam.ScreenPointToRay(rectTransform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000);
        
        Vector3 instancePos = new Vector3(hit.point.x, 1, hit.point.z);
        TeamManager.Instance.SummonUnit(instancePos, team);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            Debug.Log("Click");
            RaycastInstancingUnit();
        }
    }
}
