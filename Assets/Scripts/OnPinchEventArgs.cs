using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPinchEventArgs
{
    public float scale;
    public OnPinchEventArgs(float scaleValue)
    {
        scale = scaleValue;
    }
}
