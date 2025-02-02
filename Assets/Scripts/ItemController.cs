using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    new private Light light;
    public bool isHit, isPress;
    public string titleText;
    public Sprite image;
    GazeController2 gazeController;
    // Start is called before the first frame update
    void Start()
    {
        light.enabled = false;
        gazeController = Camera.main.GetComponent<GazeController2>();
        gazeController.OnEmptyGaze += TurnOff;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit && !light.enabled)
        {
            //Debug.Log("Turning on light...");
            TurnOn();

        }
        else if (isPress)
        {
            
        }
        else if(!isHit)
        {
            TurnOff();
        }
    }



    void TurnOn()
    {
        light.enabled = true;
    }

    void TurnOff()
    {
        light.enabled = false;
    }

    void TurnOff(object sender, EventArgs e)
    {
        light.enabled = false;
        isHit = false;
    }
}
