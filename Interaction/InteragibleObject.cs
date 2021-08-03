using System;
using UnityEngine;

public class InteragibleObject : MonoBehaviour
{
    [SerializeField] private InteractionType interactionType;

    public enum InteractionType
    {
        MakeAction, ChangeObjectPosition
    }

    public Action OnInteracted;

    public Action OnInteractionCancel;

    private bool canInteract=true;

    public  InteragibleObject.InteractionType GetInteractionType()
    {
        return interactionType;
    }

    public void InteractWhitObject()
    {
        if (canInteract)
        {
            OnInteracted?.Invoke();
        }
    }

    public void CancelInteraction()
    {
        OnInteractionCancel?.Invoke();
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }

    public bool GetCanInteract()
    {
        return canInteract;
    }
}
