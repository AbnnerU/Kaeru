using System;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDetect;

    public Action<GameObject> OnObjectEnter;

    public Action<GameObject> OnObjectExit;

    public Action OnDetectionOff;

    private void OnTriggerEnter(Collider objectCollider)
    {
        //print("enter");

        if ((layerToDetect & 1 << objectCollider.gameObject.layer) == 1 << objectCollider.gameObject.layer)
        {
            OnObjectEnter?.Invoke(objectCollider.gameObject);
        }
    }

    private void OnTriggerExit(Collider objectCollider)
    {
        //print("exit");

        if ((layerToDetect & 1 << objectCollider.gameObject.layer) == 1 << objectCollider.gameObject.layer)
        {
            OnObjectExit?.Invoke(objectCollider.gameObject);
        }
    }

    public void DetectionDisable()
    {
        OnDetectionOff?.Invoke();
    }
}
