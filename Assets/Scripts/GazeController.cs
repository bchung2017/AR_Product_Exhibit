using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;


//Implement Singleton Pattern
public class GazeController : MonoBehaviour
{
    GameObject raycastGameObject;
    GameObject[] items;
    Camera cam;
    public event EventHandler<IsPressedEventArgs> IsPressed;
    public event EventHandler IsEmptyGaze;
    public event EventHandler<IsPressedEventArgs> IsNotEmptyGaze;
    public SettingsScreenController settingsScreenController;
    bool isAutomaticMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        items = GameObject.FindGameObjectsWithTag("Item");  
        if(settingsScreenController != null)
        {
            settingsScreenController.IsToggleAutomaticMenuOn += ToggleAutomaticMenuOn;
            settingsScreenController.IsToggleAutomaticMenuOff += ToggleAutomaticMenuOff;
        }
        Debug.Log("items: " + items);
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PointerEventData pointerData = new PointerEventData(EventSystem.current);
        //pointerData.position = Input.mousePosition;
        //List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current.RaycastAll(pointerData, results);

        RaycastHit HitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out HitInfo,  100.0f))
        {
            if(HitInfo.collider.gameObject.tag == "Item")
            {
                //Debug.Log("Item " + HitInfo.collider.gameObject.name + " detected.");
                if(raycastGameObject != HitInfo.collider.gameObject)
                {
                    //New object being looked at
                    TurnOffAll();
                    raycastGameObject = HitInfo.collider.gameObject;
                    raycastGameObject.GetComponent<ItemController>().isHit = true;
                    //Debug.Log("setting " + raycastGameObject.name + " isHit true");
                }
                IsPressedEventArgs e = new IsPressedEventArgs(raycastGameObject.GetComponent<ItemController>());

                //same object being looked at
                //FIX LATER, DECOUPLE ISPRESSEDEVENTARGS FROM ISAUTOMATICMENU BOOL
                if (isAutomaticMenu)
                {
                    OnIsNotEmptyGaze(e);
                }
                if(!IsPointerOverUIObject() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isAutomaticMenu)
                {
                        
                    OnIsPressed(e);
                }
            }
            else
            {
                //Raycast against object that is not Item tag
                EventArgs e1 = new EventArgs();
                OnIsEmptyGaze(e1);
                raycastGameObject = null;
            }
            
        }
        else
        {
            raycastGameObject = null;
            EventArgs e = new EventArgs();
            OnIsEmptyGaze(e);
            if (!IsPointerOverUIObject() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                OnIsEmptyGaze(e);
            }
            //TurnOffAll();

        }   
    }

    void TurnOffAll()
    {
        foreach(GameObject i in items)
        {
            i.GetComponent<ItemController>().isHit = false;
        }
        
    }
        
    protected virtual void OnIsPressed(IsPressedEventArgs e)
    {
        EventHandler<IsPressedEventArgs> handler = IsPressed;
        handler?.Invoke(this, e);
    }

    protected virtual void OnIsEmptyGaze(EventArgs e)
    {
        EventHandler handler = IsEmptyGaze;
        handler?.Invoke(this, e);
    }

    protected virtual void OnIsNotEmptyGaze(IsPressedEventArgs e)
    {
        EventHandler<IsPressedEventArgs> handler = IsNotEmptyGaze;
        handler?.Invoke(this, e);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    void ToggleAutomaticMenuOn(object sender, EventArgs e)
    {
        isAutomaticMenu = true;
    }

    void ToggleAutomaticMenuOff(object sender, EventArgs e)
    {
        isAutomaticMenu = false;
    }
}
