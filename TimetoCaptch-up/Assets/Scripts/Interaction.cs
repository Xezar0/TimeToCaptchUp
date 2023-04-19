using UnityEngine;
public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 400f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Interactable currentInteractable;

    private void Update()
    {
        HandleInteractionCheck();
        HandleInteractionInput();
    }

    private void HandleInteractionCheck()
    {
        if(Physics.Raycast(playerCamera.ViewportPointToRay
               (new Vector3(playerCamera.rect.width/2, playerCamera.rect.height/2)), 
               out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 6 && currentInteractable == null ||
                hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID())
            {
                if(currentInteractable != null) currentInteractable.OnLoseFocus();
                
                hit.collider.TryGetComponent(out currentInteractable);
                
                if (currentInteractable) currentInteractable.OnFocus();
            }
        }
        else if(currentInteractable != null) 
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }
    private void HandleInteractionInput()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }
}
