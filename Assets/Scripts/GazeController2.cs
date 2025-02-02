using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


//Implement Singleton Pattern
public class GazeController2 : MonoBehaviour
{
    GameObject raycastGameObject;
    GameObject[] items;
    Camera cam;
    bool isGazing;
    int raycastGameObjectID;
    public event EventHandler OnEmptyGaze;
    public event EventHandler<GazeEventArgs> OnColliderGaze;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        items = GameObject.FindGameObjectsWithTag("Item");
        Debug.Log("items: " + items);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out HitInfo, 100.0f) && HitInfo.collider.gameObject.GetComponent<ItemController>() != null)
        {
            if (HitInfo.collider.gameObject.GetInstanceID() != raycastGameObjectID)
            {
                GazeEventArgs e = new GazeEventArgs(HitInfo);
                ColliderGaze(e);
                isGazing = true;
                raycastGameObjectID = HitInfo.collider.gameObject.GetInstanceID();
            }
        }
        else
        {
            if (isGazing)
            {
                EventArgs e = new EventArgs();
                EmptyGaze(e);
                isGazing = false;
                //setting to unique instance id not possible for other items
                raycastGameObjectID = gameObject.GetInstanceID(); 
            }
        }

    }

    void TurnOffAll()
    {
        foreach (GameObject i in items)
        {
            i.GetComponent<ItemController>().isHit = false;
        }
    }

    protected virtual void EmptyGaze(EventArgs e)
    {
        Debug.Log("Empty gaze");
        EventHandler handler = OnEmptyGaze;
        handler?.Invoke(this, e);
    }

    protected virtual void ColliderGaze(GazeEventArgs e)
    {
        Debug.Log("Gaze on new collider: " + e.hit.collider.gameObject.name);
        EventHandler<GazeEventArgs> handler = OnColliderGaze;
        handler?.Invoke(this, e);
    }
}
