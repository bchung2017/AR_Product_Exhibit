using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    
    Color[] colors = new Color[] {Color.white, Color.red, Color.green, Color.blue};
    private int currentColor,length;
     // Use this for initialization
    //public Camera cam;
    //private int LayerMask;
    private Vector2 touchPosition = default;
    private Camera aRcamera;
    private LayerMask mask;
     void Start () 
     {
         
         currentColor = 1; //White
         length = colors.Length;
         GetComponent<Renderer>().material.color = colors [currentColor];
         mask = LayerMask.GetMask("Interactables");
          aRcamera = Camera.main;
          Debug.Log("camara:"+aRcamera);
          
     }

     void Update () {
         


         if(Input.touchCount > 0)
         {
             Touch touch = Input.GetTouch(0);
             touchPosition = touch.position;
             
            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("SIMPLE TOUCH OK");
                
                Ray ray = aRcamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                
               
               if(Physics.Raycast(ray, out hitObject,100, mask))  
                {
                    Debug.Log("WORKING RAYCAST");
                    currentColor = (currentColor+1)%length;
                    GetComponent<Renderer>().material.color = colors[currentColor];
                }


            }
            
            
         }
     }
}
