using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemExtras : MonoBehaviour
{
    [SerializeField] private GameObject select;
    private void Start()
    {
        if(select != null) EventSystem.current.SetSelectedGameObject(select);
    }
}
