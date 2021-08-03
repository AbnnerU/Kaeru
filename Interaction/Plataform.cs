
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    //[SerializeField] private Transform parentTo;

    [SerializeField] private LayerMask layerToSetParent;

    [SerializeField] private Transform referenceOnExit;

    [SerializeField]private List<GameObject> objectsOnPlataform;

    private Transform _transform;

    private bool active;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnCollisionStay(Collision collision)
    {
        //print("ola");
        if (active)
        {           
            GameObject obj = collision.gameObject;

            if ((layerToSetParent & 1 << obj.layer) == 1 << obj.layer)
            {
                if (objectsOnPlataform.Contains(obj)==false)
                {
                    objectsOnPlataform.Add(obj);
                    //Vector3 rotation = obj.transform.localEulerAngles;
                    obj.transform.SetParent(_transform, true);

                    //obj.transform.rotation = Quaternion.Euler(Vector3.zero);

                    //obj.transform.SetParent(null, true);

                    //obj.transform.SetParent(_transform, true);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (active)
        {
            GameObject obj = collision.gameObject;

            if ((layerToSetParent & 1 << obj.layer) == 1 << obj.layer)
            {
                if (objectsOnPlataform.Contains(obj) == true)
                {
                    if (referenceOnExit)
                    {
                        obj.transform.SetParent(null, true);
                    }
                    else
                    {
                        obj.transform.SetParent(referenceOnExit, true);
                    }

                    objectsOnPlataform.Remove(obj);
                }
              
            }
        }
    }

    private void OnEnable()
    {
        active = true;
    }

    private void OnDisable()
    {
        active = false;
    }

}
