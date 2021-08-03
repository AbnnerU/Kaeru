using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWhitObjects : MonoBehaviour
{
    [SerializeField] private InputEventArea inputEventArea;

    [SerializeField] private GrabAndRelease grabAndReleaseScript;

    private void Awake()
    {
        inputEventArea.OnInputInsideArea += InputEventArea_OnInputInsideArea;
    }

    private void InputEventArea_OnInputInsideArea(GameObject obj)
    {
        InteragibleObject interagible = obj.GetComponent<InteragibleObject>();

        if(interagible!= null)
        {
            if(interagible.GetInteractionType() == InteragibleObject.InteractionType.MakeAction)
            {
                interagible.InteractWhitObject();
            }
            else if( interagible.GetInteractionType() == InteragibleObject.InteractionType.ChangeObjectPosition)
            {
                grabAndReleaseScript.GrabOrReleaseObject(obj.transform.parent.gameObject);
            }
        }
        else
        {
            print("None interagible script found");
        }

    }

}
