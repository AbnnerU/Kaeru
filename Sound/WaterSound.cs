using System;

using UnityEngine;
using UnityEngine.Events;

[Serializable] public class OnTouchWaterEvent : UnityEvent { }

public class WaterSound : MonoBehaviour
{
    [SerializeField] private Transform waterSoundObject;

    [SerializeField] private String[] tagsToDetect;

    public OnTouchWaterEvent onTouchWaterEvent;

    private void OnTriggerEnter(Collider other)
    {
        foreach (string s in tagsToDetect)
        {
            if (other.CompareTag(s))
            {
                waterSoundObject.position = other.transform.position;

                onTouchWaterEvent?.Invoke();

                break;
            }
        }
    }
}
