using System;
using UnityEngine;
public class Interaction : MonoBehaviour
{
    /// <summary>
    /// this script is on an obj whose transform.forward is taken as a direction af the raycast
    /// it can be any obj, it just so happens to be on the camera to make it easier for us to use with mouse
    /// </summary>

    [SerializeField] private float interactionDistance = 400f;
    [SerializeField] private Interactable currentInteractable;
    private LayerMask interactionMask = 6;

    private void Update()
    {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out RaycastHit hit,
                interactionDistance))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                if (currentInteractable != null) currentInteractable.OnLoseFocus();
                
                hit.collider.TryGetComponent(out currentInteractable);
                currentInteractable.OnFocus();
            }
        }
        else
        {
            if(currentInteractable != null) currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
        
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }
}
