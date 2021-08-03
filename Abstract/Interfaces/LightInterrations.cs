using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasLightInterrations
{
    void WhenLightRange(LigthRayCast lightRay);

    void WhenOutLightRange(LigthRayCast lightRay);

    //bool IsInTheLight();
}
