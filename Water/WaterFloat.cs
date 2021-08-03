using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloat : MonoBehaviour,ICanFlowPath
{
    [Header("Float Config")]
    [SerializeField] private Rigidbody rb;

    [SerializeField] private LayerMask waterLayer;

    [SerializeField] private float floatForce;

    [SerializeField] private float dumb;

    private float waterLevel;

    private float forceFactor;

    private float defaltPhysicsGravity;

    private bool OnWater=false;

    private int flowPathId;

    public int FlowPathId { get => flowPathId; set => flowPathId = value; }

    private void Awake()
    {
        defaltPhysicsGravity = Physics.gravity.y;
    }

    private void FixedUpdate()
    {
        if (OnWater)
        {
            //Float
            forceFactor = dumb - ((rb.position.y - waterLevel)) * floatForce;
            if (forceFactor > 0)
            {
                float upForce = -defaltPhysicsGravity * (forceFactor - rb.velocity.y);

                rb.AddForce(Vector3.up * upForce);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((waterLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            OnWater = true;

            this.waterLevel = other.gameObject.GetComponent<IWater>().GetWaterLevel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnWater = false;
    }
}
