using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        if (gameObject.layer != 6)
        {
            Debug.Log("Watch out, changed layer of: " + gameObject.name);
            gameObject.layer = 6;
        }
    }
    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
}