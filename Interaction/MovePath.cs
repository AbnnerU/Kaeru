using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePath : InteractiveObjectActions
{
    [SerializeField] private bool preview=true;

    [Header("Path Cofig")]
    [SerializeField] private float speed;

    [SerializeField] private bool usePathRotation=true;

    [SerializeField] private Transform[] pathPoints;

    [Header("Regrees Move Cofig")]
    [SerializeField] private bool canRegreesPath;

    [SerializeField] private float timeToStartRegress = 0.1f;

    [SerializeField] private float regressSpeed;

    [Header("When End Path")]
    [SerializeField] private EndPathActions whenEnd;

    [SerializeField] private Plataform plataform;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool disableRigidbodyOnGround;

    //[SerializeField] private float rbGravity;

    public enum EndPathActions { EnableRigidbody, JustStop, CallEventAndStop, EnablePlataform, BackToStart}

    public Action OnPathEnd;

    //public Action<bool> OnMove;
    
    private Vector3[] pathPointsWordPosition;

    private Vector3[] pathRotation;

    private Transform _transform;

    private Vector3 startPoint;

    private Vector3 startRotation;

    private Vector3 velocity;

    private Coroutine timerCoroutine;

    private DisableRBOnGround disableRBOnGround;

    private float[] pathPointsMagnitude;

    private float pathCompletePorcent;

    private int currentPointId;

    private bool startRegress;

    private bool alreadyStartedCoroutine;

    private bool canMove=false;

    private bool movementStarted;

    private bool started = false;

    private bool endPath;

    protected override void Awake()
    {
        #region atribitions
        base.Awake();

        interagibleObject.OnInteractionCancel += InteragibleObject_OnInteractionCancel;

        currentPointId = 0;

        _transform = GetComponent<Transform>();

        startPoint = _transform.position;

        startRotation = _transform.localEulerAngles;

        disableRBOnGround = GetComponent<DisableRBOnGround>();

        startRegress = false;

        movementStarted = false;

        alreadyStartedCoroutine = false;

        started = true;

        endPath = false;

        #endregion      

        if (disableRBOnGround != null)
        {
            disableRBOnGround.enabled = false;
        }

        pathPointsWordPosition = new Vector3[pathPoints.Length];

        if (usePathRotation)
        {
            pathRotation = new Vector3[pathPoints.Length];

            pathPointsMagnitude = new float[pathPoints.Length];
        }

        for(int i =0;i<pathPoints.Length;i++)
        {
            pathPointsWordPosition[i] = pathPoints[i].position;

            if (usePathRotation)
            {
                pathRotation[i] = pathPoints[i].localEulerAngles;

                if (i == 0)
                {
                    pathPointsMagnitude[i] = Vector3.Distance(pathPoints[i].position,startPoint);
                    //print(pathPointsMagnitude[i]);
                }
                else if(i>0)
                {
                    pathPointsMagnitude[i] = Vector3.Distance(pathPoints[i].position, pathPoints[i-1].position);

                    //print(pathPointsMagnitude[i]);
                }
            }


        }

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void FixedUpdate()
    {       
       
        if(canMove == false && canRegreesPath==false && movementStarted==false)
        {
            if (plataform)
            {
                plataform.enabled = false;
            }

            return;
        }
        else if (canMove == false && canRegreesPath  && movementStarted == false )
        {
            if (plataform)
            {
                plataform.enabled = false;
            }

            return;
        }
        else if(canMove==false && canRegreesPath && movementStarted == true ) //If can't move but the movement already started (Back to startPoint)
        {
            //Timer to start regress move
            if (alreadyStartedCoroutine == false)
            {

                if (plataform)
                {
                    plataform.enabled = false;
                }

                timerCoroutine = StartCoroutine(RegressTimer());
            }

            //Regress Movement
            if (startRegress == true)
            {
                if (plataform)
                {
                    plataform.enabled = true;
                }

                RegressiveMovement();

                endPath = false;
            }

        }
        else if(canMove && endPath==false)
        {
           
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }

            alreadyStartedCoroutine = false;

            startRegress = false;

            //Path Movement
            if (currentPointId < pathPointsWordPosition.Length)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, pathPointsWordPosition[currentPointId], speed * Time.deltaTime);
            }

            movementStarted = true;

            //Rotation
            if (usePathRotation && currentPointId < pathPointsMagnitude.Length)
            {
                _transform.localRotation = Quaternion.Euler(RotateObj(currentPointId, currentPointId - 1,false));
            }

            if (_transform.position == pathPointsWordPosition[currentPointId]  /*&& currentPointId < pathPointsWordPosition.Length*/)
            {
                currentPointId++;
                
                if(currentPointId== pathPointsWordPosition.Length)
                {
                    ChoosePathEndAction();
                    endPath = true;
                }
            }
        }
    
    }  

    private void RegressiveMovement()
    {
        if (currentPointId == 0)
        {
            //Move to startPoint
            _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, startPoint, regressSpeed * Time.deltaTime);

            if (_transform.position == startPoint)
            {
                movementStarted = false;
            }
        }
        else
        {
            //Move to the last waypoint
            _transform.position = Vector3.MoveTowards(_transform.position, pathPointsWordPosition[currentPointId - 1], regressSpeed * Time.deltaTime);

            if (_transform.position == pathPointsWordPosition[currentPointId - 1])
            {
                currentPointId--;
            }
        }

        //Rotation
        if (usePathRotation && currentPointId < pathPointsMagnitude.Length)
        {
            _transform.localRotation = Quaternion.Euler(RotateObj(currentPointId - 1, currentPointId, true));
            //print(currentPointId - 1 + " , " + currentPointId);
        }
    }

    private Vector3 RotateObj(int currentPoint,int lastPoint, bool regressMovement)
    {                            //currentPoint-1  //CurrentPoint
        float currentDistance = 0;

        Vector3 difference = Vector3.zero;

        Vector3 rotation=Vector3.zero;

        if (regressMovement == false)
        {
            currentDistance = Vector3.Distance(_transform.position, pathPointsWordPosition[currentPoint]);

            if (currentPointId == 0)
            {
                difference = startRotation;
            }
            else
            {
                difference = pathRotation[lastPoint];
            }

            float pathTakenPorcentage = (currentDistance * 100) / pathPointsMagnitude[currentPoint];

            Vector3 finalDifference = difference - (difference * ((100 - pathTakenPorcentage) / 100));

            rotation = finalDifference + (pathRotation[currentPoint] * ((100 - pathTakenPorcentage) / 100));

        }
        else
        {
            if(currentPointId == 0)
            {
                currentDistance = Vector3.Distance(_transform.position, startPoint);
                difference = pathRotation[lastPoint];
            }
            else
            {
                currentDistance = Vector3.Distance(_transform.position, pathPointsWordPosition[currentPoint]);
                difference = pathRotation[lastPoint];
            }

            float pathTakenPorcentage = (currentDistance * 100) / pathPointsMagnitude[lastPoint];

            //print(pathTakenPorcentage);

            Vector3 finalDifference = difference - (difference * ((100 - pathTakenPorcentage) / 100));

            if (currentPointId == 0)
            {
                rotation = finalDifference + (startRotation * ((100 - pathTakenPorcentage) / 100));
            }
            else
            {
                rotation = finalDifference + (pathRotation[currentPoint] * ((100 - pathTakenPorcentage) / 100));
            }

        }

            return rotation;
        
    }

    private void ChoosePathEndAction()
    {
        MovePath movePath = this;

        switch (whenEnd)
        {
            case EndPathActions.JustStop:

                interagibleObject.SetCanInteract(false);

                movePath.enabled = false;

                if (plataform)
                {
                    plataform.enabled = false;
                }

                break;

            case EndPathActions.CallEventAndStop:

                interagibleObject.SetCanInteract(false);

                OnPathEnd?.Invoke();

                movePath.enabled = false;

                if (plataform)
                {
                    plataform.enabled = false;
                }

                break;

            case EndPathActions.EnableRigidbody:

                interagibleObject.SetCanInteract(false);

                OnPathEnd?.Invoke();

                rb.isKinematic = false;

                movePath.enabled = false;


                if (plataform)
                {
                    plataform.enabled = true;
                }

                if (disableRigidbodyOnGround)
                {
                    if (disableRBOnGround != null)
                    {
                        disableRBOnGround.enabled = true;
                    }
                }

                break;

            case EndPathActions.EnablePlataform:

                interagibleObject.SetCanInteract(false);

                movePath.enabled = false;

                if (plataform)
                {
                    plataform.enabled = true;
                }

                break;

            case EndPathActions.BackToStart:

                if (plataform)
                {
                    plataform.enabled = false;
                }

                StopAllCoroutines();

                break;
        }
    }

    #region Timers

    IEnumerator RegressTimer()
    {
        alreadyStartedCoroutine = true;

        yield return new WaitForSeconds(timeToStartRegress);

        startRegress = true;

        StopCoroutine(timerCoroutine);
    }

    #endregion

    #region Interaction

    protected override void InteragibleObject_OnInteracted()
    {
        if (plataform)
        {
            plataform.enabled = true;
        }

        if (endPath==false)
        {
            //OnMove?.Invoke(true);
        }

        canMove = true;
    }

    private void InteragibleObject_OnInteractionCancel()
    {
        if (endPath==false)
        {
            //OnMove?.Invoke(false);
        }

        canMove = false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        if (preview)
        {
            if (started)
            {
                if (pathPointsWordPosition != null)
                {
                    Gizmos.color = Color.yellow;

                    for (int i = 0; i < pathPointsWordPosition.Length; i++)
                    {
                        if (i == 0)
                        {
                            Gizmos.DrawLine(startPoint, pathPointsWordPosition[i]);
                        }
                        else
                        {
                            Gizmos.DrawLine(pathPointsWordPosition[i - 1], pathPointsWordPosition[i]);
                        }
                    }

                    Gizmos.color = Color.white;
                    for (int i = 0; i < pathPointsWordPosition.Length; i++)
                    {
                        Gizmos.DrawRay(pathPointsWordPosition[i] , pathPoints[i].forward);
                        Vector3 right = Quaternion.LookRotation(pathPoints[i].forward) * Quaternion.Euler(0, 180 + 20, 0) * new Vector3(0, 0, 1);
                        Vector3 left = Quaternion.LookRotation(pathPoints[i].forward) * Quaternion.Euler(0, 180 - 20, 0) * new Vector3(0, 0, 1);
                        Gizmos.DrawRay(pathPointsWordPosition[i] + pathPoints[i].forward, right * 0.25f);
                        Gizmos.DrawRay(pathPointsWordPosition[i] + pathPoints[i].forward, left * 0.25f);
                    }
                }
            }
            else
            {
                Gizmos.color = Color.yellow;

                for (int i = 0; i < pathPoints.Length; i++)
                {
                    if (i == 0)
                    {
                        Gizmos.DrawLine(transform.position, pathPoints[i].position);
                    }
                    else
                    {
                        Gizmos.DrawLine(pathPoints[i - 1].position, pathPoints[i].position);
                    }
                }

                Gizmos.color = Color.white;

                for (int i = 0; i < pathPoints.Length; i++)
                {
                    Gizmos.DrawRay(pathPoints[i].position, pathPoints[i].forward);
                    Vector3 right = Quaternion.LookRotation(pathPoints[i].forward) * Quaternion.Euler(0, 180 + 20, 0) * new Vector3(0, 0, 1);
                    Vector3 left = Quaternion.LookRotation(pathPoints[i].forward) * Quaternion.Euler(0, 180 - 20, 0) * new Vector3(0, 0, 1);
                    Gizmos.DrawRay(pathPoints[i].position + pathPoints[i].forward, right * 0.25f);
                    Gizmos.DrawRay(pathPoints[i].position + pathPoints[i].forward, left * 0.25f);
                }
            }
        }
    }

    public bool GetEndPath()
    {
        return endPath;
    }

    public void SetPlataformLastOnPosition()
    {
        _transform.position = pathPointsWordPosition[pathPointsWordPosition.Length - 1];

        ChoosePathEndAction();
        endPath = true;
    }
}
