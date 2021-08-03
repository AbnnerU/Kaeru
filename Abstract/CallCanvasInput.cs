using System;
using UnityEngine;

public abstract class CallCanvasInput:MonoBehaviour
{
    public Action<bool> inputTextEvent;

    public Action<bool> inputEspecial;
}
