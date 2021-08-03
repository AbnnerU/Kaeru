using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWaterFlow : WaterFlowBasics,IWater
{
    [SerializeField] private bool preview;
    [SerializeField] private Color previewColor;

    [SerializeField] private List<Rigidbody> objectsOnWater;


    private void OnTriggerEnter(Collider other)
    {
        WaterFloat floatObject = other.gameObject.GetComponent<WaterFloat>();

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if(floatObject != null && objectsOnWater.Contains(rb) == false)
        {
            objectsOnWater.Add(rb);
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (objectsOnWater.Contains(rb) == true)
        {
            objectsOnWater.Remove(rb);
        }
    }

    private void FixedUpdate()
    {
        if (objectsOnWater == null || waterSpeed==0)
        {
            return;
        }

        foreach(Rigidbody r in objectsOnWater)
        { 
            //Move 
            if(r.velocity.magnitude < maxObjectSpeedOnWater)
            {
                r.AddForce(transform.forward * waterSpeed);
            }
        }

    }

    public float GetWaterLevel()
    {
        return waterLevel;
    }

    private void OnDrawGizmos()
    {
        if (preview)
        {
            //BOX
            Gizmos.color = previewColor;

            Gizmos.DrawCube(transform.position + GetComponent<BoxCollider>().center, new Vector3(GetComponent<BoxCollider>().size.x * transform.localScale.x,
                GetComponent<BoxCollider>().size.y * transform.localScale.y, GetComponent<BoxCollider>().size.z * transform.localScale.z));
        }

        //Direction Arrow
        if (waterSpeed != 0)
        {
            Gizmos.DrawRay(transform.position + (Vector3.up * (boxCollider.size.y / 2)), transform.forward);
            Vector3 right = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 180 + 20, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 180 - 20, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(transform.position + (Vector3.up * (boxCollider.size.y / 2)) + transform.forward, right * 0.25f);
            Gizmos.DrawRay(transform.position + (Vector3.up * (boxCollider.size.y / 2)) + transform.forward, left * 0.25f);
        }
    }

  
}
