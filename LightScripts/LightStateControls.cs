using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStateControls : InteractiveObjectActions
{
    [SerializeField] private LightState[] lightStates;

    protected override void InteragibleObject_OnInteracted()
    {
        print("Control");
        foreach (LightState ls in lightStates)
        {
            ls.InvertLightState();
        }
    }
}
