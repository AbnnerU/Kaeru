using System;
using UnityEngine;


public class SmashDetection : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDetect;

    public Action<bool> OnDetectCollision;



    private void OnTriggerStay(Collider other)
    {
        if ((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            OnDetectCollision?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            OnDetectCollision?.Invoke(false);
        }
    }
}
