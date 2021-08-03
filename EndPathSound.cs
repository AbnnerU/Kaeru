using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPathSound : MonoBehaviour
{
    public OnTouch onEnd;
    private MovePath movePath;

    private void Awake()
    {
        movePath = GetComponent<MovePath>();
        if (movePath)
        {
            movePath.OnPathEnd += MovePath_OnEnd; 
        }
    }

    private void MovePath_OnEnd()
    {
        onEnd?.Invoke();
    }
}
