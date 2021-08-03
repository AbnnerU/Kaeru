using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndRelease : MonoBehaviour
{
    //[SerializeField] private InputEventArea inputEventArea;

    [SerializeField] private Collider triggerCollider;

    [Header("Config Grab")]

    [SerializeField] private Transform lightPositionReference;

    [Header("Config Release")]

    [SerializeField] private Transform lightContainer;

    public Action<bool> OnGrabing;
    //private GameObject objectGrabbed;

    private bool alreadyHoldingObject=false;

    private bool grabMainLight=false;

    //private void Awake()
    //{
    //    inputEventArea.OnInputInsideArea += InputEventArea_OnInputInsideArea;
    //}

   public void GrabOrReleaseObject(GameObject objectGrabbed)
    {
        if (alreadyHoldingObject == false)
        {

            if (objectGrabbed != null)
            {
                OnGrabing?.Invoke(true);

                alreadyHoldingObject = true;

                Rigidbody rb = objectGrabbed.GetComponent<Rigidbody>();

                DisableSmash disableSmash = objectGrabbed.GetComponent<DisableSmash>();

                if (disableSmash != null)
                {
                    disableSmash.EnableSmash(false);
                }

                if (rb!=null)
                {
                    rb.isKinematic = true;

                    rb.velocity = Vector3.zero;

                    rb.useGravity = false;
                }

                if (objectGrabbed.CompareTag("Main light"))
                {
                    grabMainLight = true;
                }
                else
                {
                    grabMainLight = false;
                }

                objectGrabbed.GetComponent<Detection>()?.DetectionDisable();

                objectGrabbed.transform.SetParent(lightPositionReference, true);

                objectGrabbed.transform.localPosition = Vector3.zero;

                objectGrabbed.GetComponent<Collider>().enabled = false;

                triggerCollider.enabled = false;
            }
        }
        else
        {
            OnGrabing?.Invoke(false);

            alreadyHoldingObject = false;

            grabMainLight = false;

            objectGrabbed.transform.SetParent(lightContainer, true);

            objectGrabbed.GetComponent<Collider>().enabled = true;

            Rigidbody rb = objectGrabbed.GetComponent<Rigidbody>();

            DisableSmash disableSmash = objectGrabbed.GetComponent<DisableSmash>();

            if (disableSmash != null)
            {
                disableSmash.EnableSmash(true);
            }

            if (rb != null)
            {
                rb.isKinematic = false;

                rb.velocity = Vector3.zero;

                rb.useGravity= true;
            }

            triggerCollider.enabled = true;
            objectGrabbed = null;
        }

    }

    public bool GetHoldingObject()
    {
        return alreadyHoldingObject;
    }

    public bool GetGrabMainLight()
    {
        return grabMainLight;
    }

}
