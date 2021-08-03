using System;

using UnityEngine;
using UnityEngine.Events;
public class InputEventArea : CallCanvasInput
{
    [SerializeField] private LayerMask layerToDetect;

    public Action<GameObject> OnInputInsideArea;

    private InputController inputController;

    private GameObject objectInsideArea;

    private bool active;

    private void OnEnable()
    {
        active = true;
    }

    private void OnDisable()
    {
        active = false;

        inputEspecial?.Invoke(false);
        inputTextEvent?.Invoke(false);
    }

    void Awake()
    {
        inputController = FindObjectOfType<InputController>();

        inputController.OnInteractEvent += InputController_OnInteractEvent;
    }

    private void InputController_OnInteractEvent()
    {
        if (objectInsideArea!=null && active)
        {
            OnInputInsideArea?.Invoke(objectInsideArea);

            inputEspecial?.Invoke(false);

            inputTextEvent?.Invoke(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer && active)
        {
            objectInsideArea = other.gameObject;

            if (other.CompareTag("Homenagem"))
            {
                inputEspecial?.Invoke(true);
            }
            else
            {
                inputTextEvent?.Invoke(true);
            }
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if ((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer && active)
        {
            objectInsideArea = null;

            if (other.CompareTag("Homenagem"))
            {
                inputEspecial?.Invoke(false);
            }
            else
            {
                inputTextEvent?.Invoke(false);
            }
        }

        
    }

    //public bool GetActive()
    //{
    //    return active;
    //}
}
