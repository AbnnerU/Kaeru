using UnityEngine;

public abstract class InteractiveObjectActions : MonoBehaviour
{
    [SerializeField] protected InteragibleObject interagibleObject;

    protected virtual void Awake()
    {
        interagibleObject.OnInteracted += InteragibleObject_OnInteracted;
    }

    protected abstract void InteragibleObject_OnInteracted();
   
}
