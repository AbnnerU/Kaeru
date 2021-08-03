using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthRayCast : MonoBehaviour
{
    [SerializeField] private Detection detection;

    [SerializeField] private List<GameObject> objectsOnRange;

    [Header("Raycast properties")]

    [SerializeField] private float raycastRange;

    [SerializeField] private LayerMask allInteractiveLayers;

    private Transform _transform;

    private void Start()
    {
        detection.OnObjectEnter += Detection_OnObjectEnter;

        detection.OnObjectExit += Detection_OnObjectExit;

        detection.OnDetectionOff += Detection_OnDetectionOff;

        _transform = GetComponent<Transform>();

    }

    private void OnEnable()
    {
        //detection.OnObjectEnter += Detection_OnObjectEnter;

        //detection.OnObjectExit += Detection_OnObjectExit;
    }

    private void FixedUpdate()
    {
        if(objectsOnRange.Count == 0)
        {
            //print("No objects");
            return;
        }
    
        foreach(GameObject g in objectsOnRange)
        {
            if (g.GetComponent<Collider>().enabled == false)
            {
                objectsOnRange.Remove(g);

                return;
            }

            IHasLightInterrations lightInterrations = g?.GetComponent<IHasLightInterrations>();

            RaycastHit hit;
            
            if (Physics.Raycast(_transform.position, (g.transform.position - _transform.position) ,out hit, raycastRange, allInteractiveLayers))
            {
                if (hit.collider.CompareTag("Player"))
                {             
                    Debug.DrawRay(_transform.position, g.transform.position - _transform.position, Color.yellow);

                    
                     lightInterrations?.WhenLightRange(this);
                    

                    //print("On Light Range");
                }
                else //maybe the player is in range, but there is an obstacle in the way
                {
                    
                     lightInterrations?.WhenOutLightRange(this);
                    

                    //print("Out Light Range");
                }
            }
            //else //Player Out of Range
            //{
            //    if (lightInterrations?.IsInTheLight() == true)
            //    {
            //        g.GetComponent<IHasLightInterrations>()?.WhenOutLightRange();
            //    }

            //    print("Out Light Range");
            //}

        }



    }


    private void Detection_OnObjectEnter(GameObject target)
    {
        if (objectsOnRange.Contains(target))
        {
            return;
        }

        IHasLightInterrations lightInterrations = target?.GetComponent<IHasLightInterrations>();

        if (lightInterrations != null)
        {
            lightInterrations.WhenLightRange(this);
        }

        objectsOnRange.Add(target);
    }

    private void Detection_OnObjectExit(GameObject target)
    {
        objectsOnRange.Remove(target);

        IHasLightInterrations lightInterrations = target?.GetComponent<IHasLightInterrations>();


        if (lightInterrations != null)
        {
            lightInterrations.WhenOutLightRange(this);
        }
    }

    private void Detection_OnDetectionOff()
    {
        foreach (GameObject g in objectsOnRange)
        {
            IHasLightInterrations lightInterrations = g?.GetComponent<IHasLightInterrations>();

           
            lightInterrations.WhenOutLightRange(this);

        }

        objectsOnRange.Clear();
    }
}
