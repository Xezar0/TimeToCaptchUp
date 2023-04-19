using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIKeyBoard : Interactable
{
    private Outline outline;
    [SerializeField] private MoveDirection inputDirection;
    [SerializeField] private bool isSelect;
    public override void Awake()
    {
        base.Awake();
        outline = GetComponent<Outline>();
        outline.enabled = true;
        StartCoroutine(OutlineDelay());
    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        if(inputDirection != MoveDirection.None) Move(inputDirection);
        if (isSelect)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, null, ExecuteEvents.submitHandler);
        }
    }
    public void Move(MoveDirection direction)
    {
        AxisEventData data = new AxisEventData(EventSystem.current);
        data.moveDir = direction;
        data.selectedObject = EventSystem.current.currentSelectedGameObject;
        ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
        
        Debug.Log("move: " + direction);
        Debug.Log(EventSystem.current);
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    IEnumerator OutlineDelay()
    {
        yield return new WaitForSeconds(0.1f);
        outline.enabled = false;
    }
}
