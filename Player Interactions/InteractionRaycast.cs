using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]public class OnInteracting: UnityEvent<bool> { }

public class InteractionRaycast : CallCanvasInput
{
    [SerializeField] private InputController inputController;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private float raycastRange;

    [SerializeField] private LayerMask layerToInteract;

    public Action<bool> OnEnableInteraction;

    public Action<bool> OnTouchObject;

    public OnInteracting onInteracting;

    private InteragibleObject interactingWithObject;

    //private bool aiming;

    private bool shooting;

    private bool active;

    private void OnEnable()
    {
        OnEnableInteraction?.Invoke(true);

        active = true;

        //aiming = false;

        shooting = false;
    }

    private void OnDisable()
    {
        OnEnableInteraction?.Invoke(false);

        active = false;

        //aiming = false;

        shooting = false;

        inputTextEvent?.Invoke(false);
    }

    private void Awake()
    {
        //inputController.OnAimEvent += InputController_OnAimEvent;

        inputController.OnShootEvent += InputController_OnShootEvent;
    }

    private void FixedUpdate()
    {
        //if (aiming == false)
        //{
        //    StopInteraction();
        //    return;
        //}

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        
        if (shooting)
        {        
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
            if (Physics.Raycast(ray, out hit, raycastRange, layerToInteract))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                InteragibleObject interagible = hit.collider.gameObject.GetComponent<InteragibleObject>();

                if (interagible != null)
                {
                    interagible.InteractWhitObject();

                    interactingWithObject = interagible;

                    if (interagible.GetCanInteract())
                    {
                        onInteracting?.Invoke(true);
                    }
                    else
                    {
                        onInteracting?.Invoke(false);
                    }

                    if (interagible.GetCanInteract())
                    {
                        inputTextEvent?.Invoke(true);
                        OnTouchObject?.Invoke(true);
                    }
                    else
                    {
                        inputTextEvent?.Invoke(false);
                        OnTouchObject?.Invoke(false);
                    }
                }
                else
                {
                    inputTextEvent?.Invoke(false);

                    onInteracting?.Invoke(false);

                    StopInteraction();
                }
            }
            else
            {
                inputTextEvent?.Invoke(false);

                onInteracting?.Invoke(false);

                StopInteraction();
            }
        }
        else
        {
            onInteracting?.Invoke(false);

            if (Physics.Raycast(ray, out hit, raycastRange, layerToInteract))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.yellow);
                InteragibleObject interagible = hit.collider.gameObject.GetComponent<InteragibleObject>();

                if (interagible != null)
                {
                    //print("HOLA");
                    if (interagible.GetCanInteract())
                    {
                        inputTextEvent?.Invoke(true);
                        OnTouchObject?.Invoke(true);
                    }
                    else
                    {
                        OnTouchObject?.Invoke(false);
                    }              
                }
                else
                {
                    inputTextEvent?.Invoke(false);

                    OnTouchObject?.Invoke(false);
                }

            }
            else
            {
                inputTextEvent?.Invoke(false);

                OnTouchObject?.Invoke(false);
            }
           
            StopInteraction();
        }
        
    }

    public void StopInteraction()
    {
        if (interactingWithObject != null)
            interactingWithObject.CancelInteraction();

        interactingWithObject = null;
    }

    //private void InputController_OnAimEvent(float aimImput)
    //{
    //    if (active)
    //    {
    //        if (aimImput > 0)
    //        {
    //            aiming = true;
    //        }
    //        else
    //        {
    //            aiming = false;

    //            inputTextEvent?.Invoke(false);
    //        }

    //        OnAim?.Invoke(aiming);
    //    }
    //}

    private void InputController_OnShootEvent(float shootImput)
    {
        if (active)
        {
            if (shootImput > 0)
            {
                shooting = true;
            }
            else
            {
                shooting = false;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        Vector3 aaa = new Vector3(0, 0, raycastRange);
        Gizmos.DrawLine(ray.origin, ray.origin+aaa);
    }
}
