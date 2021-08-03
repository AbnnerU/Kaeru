using System;
using UnityEngine;

public class CanvasInputsTexts : MonoBehaviour
{
    [SerializeField] private string buttonInputName;
    [SerializeField] private string actionDescription;

    private CallCanvasInput callCanvasInput;
    private CanvasManeger_Game canvasManeger;


    private void Awake()
    {
        canvasManeger = FindObjectOfType<CanvasManeger_Game>();

        callCanvasInput = GetComponent<CallCanvasInput>();

        callCanvasInput.inputTextEvent += OnInputEvent;

        callCanvasInput.inputEspecial += OnInputEspecial;
        
    }

    private void OnInputEvent(bool showText)
    {

        if (showText)
        { 
            canvasManeger.ShowInputText(buttonInputName, actionDescription);

            //print("on");
        }
        else
        {
            //print("Off");
            canvasManeger.HideInputText();
        }
    }

    private void OnInputEspecial(bool showText)
    {
        if (showText)
        {
            canvasManeger.ShowEspecial("'Nosso eterno herói'");
            //print("on");
        }
        else
        {
            //print("Off");
            canvasManeger.HideInputText();
        }
    }
}
