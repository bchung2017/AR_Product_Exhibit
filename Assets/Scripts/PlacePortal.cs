using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;

public class PlacePortal : PlaceObject
{
    EnvironmentController environmentController;
    PortalController portalController;
    public Transform placementPoint;
    public GameObject enterPrompt;
    
    // public GameObject activeScreen;
    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        environmentController = GameObject.FindGameObjectWithTag("Environment").GetComponent<EnvironmentController>();
        portalController = environmentController.gameObject.GetComponent<PortalController>();
        //button.GetComponent<Button>().onClick.AddListener(Activate);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        if (placementPoseIsValid)
        {
            //ARAnchor aRAnchor = aRAnchorManager.AttachAnchor(chosenARPlane, placementPose);
            objectToPlace.SetActive(true);
            //objectToPlace.transform.parent = aRAnchor.transform;
            Vector3 displacement = Vector3.Normalize(placementPose.position - cam.transform.position);
            Quaternion objectRot = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));
            objectToPlace.transform.rotation = objectRot;
            portalController.SetPosition(placementPose.position);
            environmentController.height = objectToPlace.transform.position.y;
            // activeScreen.SetActive(true);
            buttonPressed = true;
            placementPoseIsValid = false;
            Hide();
            scanSprite.SetActive(false);
            enterPrompt.SetActive(true);
        }
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs e)
    {
        //foreach(ARPlane a in e.updated)
        //{
        //    if(base.chosenARPlane == a)
        //        Debug.Log("plane updated: " + a + ", plane size: " + a.size.x + ", " + a.size.y);
        //}

        //if(base.chosenARPlane != null)
        //{
        //    foreach (ARTrackable a in aRPlaneManager.trackables)
        //    {
        //        if (a.GetType() == typeof(ARPlane))
        //        {
        //            if ((ARPlane)a != base.chosenARPlane)
        //            {
        //                Debug.Log("chosenARPlane: " + chosenARPlane + ", setting inactive: " + a);
        //                a.gameObject.SetActive(false);
        //            }
                        
        //        }
        //        else
        //        {
        //            a.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
}
