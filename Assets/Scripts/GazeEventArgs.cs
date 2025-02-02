using UnityEngine;
using System;

public class GazeEventArgs : EventArgs
{
   public GazeEventArgs(RaycastHit hit)
   {
       this.hit = hit;
   } 

   public RaycastHit hit;
}
