using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPathFlow : WaterFlowBasics, IWater
{
    [Header("Flow Config")]
    [SerializeField] private bool preview = true;

    [SerializeField] private Color previewColor;

    [SerializeField] private Vector2 radiusTolerance;

    [SerializeField] private List<Transform> objectsOnWater;

    [SerializeField] private Transform[] flowPoints;

    [SerializeField] private List<WaterFloat> waterFloatComponents;

    private Vector3[] flowPointsWordPosition;



    private bool started = false;

    protected override void Awake()
    {
        base.Awake();

        int id = 0;

        flowPointsWordPosition = new Vector3[flowPoints.Length];

        foreach (Transform t in flowPoints)
        {
            flowPointsWordPosition[id] = t.position;

            id++;
        }
    }

    protected void FixedUpdate()
    {
        if(objectsOnWater == null)
        {
            return;
        }

        //print("Hola");
        for(int i =0; i< objectsOnWater.Count; i++)
        {
            int direction = waterFloatComponents[i].FlowPathId;

            objectsOnWater[i].position = Vector3.MoveTowards(objectsOnWater[i].position, new Vector3(flowPointsWordPosition[direction].x, objectsOnWater[i].position.y, flowPointsWordPosition[direction].z), 
                waterSpeed * Time.deltaTime);

            //Object On Range
            if (ObjectOnPointRange(objectsOnWater[i].position, direction))
            {
                waterFloatComponents[i].FlowPathId++;

                if (waterFloatComponents[i].FlowPathId == flowPointsWordPosition.Length)
                {
                    waterFloatComponents[i].FlowPathId = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WaterFloat waterFloat = other.gameObject.GetComponent<WaterFloat>();
        //print(waterFloat);

        Transform objTransform = other.gameObject.GetComponent<Transform>();

        if (waterFloat!=null && objectsOnWater.Contains(objTransform) == false && waterFloatComponents.Contains(waterFloat)==false)
        {
            print("Enter ee");
            objectsOnWater.Add(objTransform);

            waterFloatComponents.Add(waterFloat);

            waterFloat.FlowPathId = GetCloserPointId(objTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform objTransform = other.gameObject.GetComponent<Transform>();

        if (objectsOnWater.Contains(objTransform))
        {
            objectsOnWater.Remove(objTransform);
        }
    }

    private bool ObjectOnPointRange(Vector3 objPosition, int pointID)
    {
        if (objPosition.x < flowPointsWordPosition[pointID].x + radiusTolerance.x && objPosition.x > flowPointsWordPosition[pointID].x - radiusTolerance.x
            && objPosition.z < flowPointsWordPosition[pointID].z + radiusTolerance.y && objPosition.z > flowPointsWordPosition[pointID].z - radiusTolerance.y)
        {
            //print("true");
            return true;
        }
        else
        {
            //print("False");
            return false;
        }
    }

    private int GetCloserPointId(Transform pointA)
    {
        int closerId=0;

        float currentCloserDistance=0;

        Vector3 pointAPosition = pointA.position;

        for(int i=0 ; i<flowPointsWordPosition.Length ; i++)
        {
            if (i == 0)
            {
                float distance = (pointAPosition - flowPointsWordPosition[i]).magnitude;

                currentCloserDistance = distance;

                closerId = 0;
            }
            else
            {
                float distance = (pointAPosition - flowPointsWordPosition[i]).magnitude;

                if(distance < currentCloserDistance)
                {
                    currentCloserDistance = distance;

                    closerId = i;
                }

            }
        }

        if (closerId == flowPointsWordPosition.Length - 1)
        {
            print("0");
            return 0;
        }
        else
        {
            print(closerId);
            return closerId + 1;
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

            //PATH
            if (started)
            {
                if (flowPointsWordPosition != null)
                {
                    Gizmos.color = Color.yellow;

                    for (int i = 0; i < flowPointsWordPosition.Length; i++)
                    {
                        if (i == 0)
                        {
                            Gizmos.DrawLine(flowPointsWordPosition[0], flowPointsWordPosition[i]);
                        }
                        else
                        {
                            Gizmos.DrawLine(flowPointsWordPosition[i - 1], flowPointsWordPosition[i]);
                        }
                    }
                }
            }
            else
            {
                Gizmos.color = Color.yellow;

                for (int i = 0; i < flowPoints.Length; i++)
                {
                    if (i == 0)
                    {
                        Gizmos.DrawLine(flowPoints[0].position, flowPoints[i].position);
                    }
                    else
                    {
                        Gizmos.DrawLine(flowPoints[i - 1].position, flowPoints[i].position);
                    }
                }
            }

            //Tolerance radius
            Gizmos.color = Color.red;
            if (started)
            {
                if (flowPointsWordPosition != null)
                {
                  
                    for (int i = 0; i < flowPointsWordPosition.Length; i++)
                    {                      
                         Gizmos.DrawCube(flowPointsWordPosition[i], new Vector3(radiusTolerance.x,0,radiusTolerance.y));                      
                    }
                }
            }
            else
            {
                for (int i = 0; i < flowPoints.Length; i++)
                {
                    Gizmos.DrawCube(flowPoints[i].position, new Vector3(radiusTolerance.x, 0, radiusTolerance.y));
                }
            }
        }
    }
}
