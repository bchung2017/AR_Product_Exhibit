using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RaycastEventArgs : EventArgs
{
    public RaycastEventArgs(RaycastHit hit)
   {
       this.hit = hit;
   } 
    public RaycastHit hit;
}
