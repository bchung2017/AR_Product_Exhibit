using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using System.Linq;
using System.IO;
using TMPro;
using RainbowArt.CleanFlatUI;

public class PlaceObject: MonoBehaviour
{
    public XROrigin origin;
    protected ARRaycastManager raycastManager;
    public ARPlaneManager aRPlaneManager;
    public ARPointCloudManager aRPointCloudManager;
    public GameObject placementIndicator;       //gameobject that visualizes where to set object
    public GameObject objectToPlace;            //object to place in placementIndicator's location
    public GameObject button;
    public GameObject scanSprite, debugMessage;
    public Camera cam;
    protected bool placementPoseIsValid = false;
    protected Pose placementPose;                         //pose of placementIndicator
    protected bool buttonPressed = false;
    protected ARAnchorManager aRAnchorManager;
    protected ARPlane chosenARPlane;
    protected Animator buttonAnimator;
    protected Animator instructionAnimator;
    public float targetAreaAmount;
    float scannedAreaAmount = 0f;
    //public TextMeshProUGUI scanPercentageText;
    public ProgressBar scanProgressBar;
    bool isTargetScanAreaReached;
    public float minimumDistance;

    private void OnEnable()
    {
        isTargetScanAreaReached = false;
        aRPlaneManager.planesChanged += OnPlanesChanged;
    }

    protected virtual void Awake()
    {
        button.GetComponent<Button>().onClick.AddListener(() => Activate());
        origin = GetComponent<XROrigin>();
        aRAnchorManager = GetComponent<ARAnchorManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        buttonAnimator = button.GetComponent<Animator>();
        instructionAnimator = scanSprite.GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        objectToPlace.SetActive(false);
    }

    protected virtual void Update()
    {
        if (!buttonPressed)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
    }

    private void OnDisable()
    {
        aRPlaneManager.planesChanged -= OnPlanesChanged;
        isTargetScanAreaReached = true;
    }

    //Get raycast hits and set placementPose to the first hit pose
    private void UpdatePlacementPose()
    {
        var screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            //if (hits.First().trackable is ARPlane aRPlane)
            //{
            //    chosenARPlane = aRPlane; //make sure if it is a plane
            //}
            if (isTargetScanAreaReached)
            {
                chosenARPlane = (ARPlane)hits.First().trackable;
            }
            placementPose = hits[0].pose;
        }
    }

    //set location of placementIndicator to placementPose
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid && chosenARPlane != null)
        {
            Vector3 normalDisplacment = Vector3.Normalize(placementPose.position - cam.transform.position);
            Vector3 displacement = placementPose.position - cam.transform.position;
            Debug.Log("displacement: " + displacement.magnitude);
            if(displacement.magnitude > minimumDistance)
            {
                Quaternion objectRot = Quaternion.LookRotation(new Vector3(normalDisplacment.x, 0, normalDisplacment.z));
                placementIndicator.transform.SetPositionAndRotation(placementPose.position, objectRot);
                Show();
            }
            else
            {
                debugMessage.GetComponent<TextMeshProUGUI>().text = "Look more up";
                Hide();
            }
              
        }
        else
        {
            Hide();
        }
    }
    public virtual void Activate()
    {
        if (placementPoseIsValid)
        {
            ARAnchor aRAnchor = aRAnchorManager.AttachAnchor(chosenARPlane, placementPose);
            objectToPlace.SetActive(true);
            objectToPlace.transform.parent = aRAnchor.transform;
            Vector3 displacement = Vector3.Normalize(placementPose.position - cam.transform.position);
            Quaternion objectRot = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));
            objectToPlace.transform.SetPositionAndRotation(placementPose.position, objectRot);
            buttonPressed = true;
            placementPoseIsValid = false;
            Hide();
            scanSprite.SetActive(false);
        }
    }

    public void Deactivate()
    {
        Show();
        origin.GetComponent<ARPlaneManager>().enabled = true;
        origin.GetComponent<ARPointCloudManager>().enabled = true;
        scanSprite.SetActive(true);
        scanProgressBar.gameObject.SetActive(true);
        scannedAreaAmount = 0f;
    }

    protected void Show()
    {
        debugMessage.GetComponent<TextMeshProUGUI>().text = "Slowly scan a surface";
        placementIndicator.GetComponent<PlacementIndicatorController>().Show();
        buttonAnimator.SetBool("Show", true);
        instructionAnimator.SetBool("Show", false);
    }

    protected void Hide()
    {
        placementIndicator.GetComponent<PlacementIndicatorController>().Hide();
        buttonAnimator.SetBool("Show", false);
        instructionAnimator.SetBool("Show", true);
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs e)
    {
        float currentAreaAmount = 0f;


        foreach (ARPlane a in e.updated)
        {
            //Debug.Log("plane updated: " + a + ", plane size: " + a.size.x + ", " + a.size.y);
            float planeArea = a.size.x * a.size.y;
            currentAreaAmount += planeArea;
        }

        //Debug.Log("current area: " + currentAreaAmount);

        if(scannedAreaAmount < currentAreaAmount)
        {
            scannedAreaAmount = currentAreaAmount;
        }

        if(scannedAreaAmount > targetAreaAmount)
        {
            isTargetScanAreaReached = true;
            foreach (ARPointCloud a in aRPointCloudManager.trackables)
            {
                a.gameObject.SetActive(false);
            }
            scanProgressBar.gameObject.SetActive(false);
        }

        float scanPercentage = scannedAreaAmount / targetAreaAmount * 100;
        scanProgressBar.CurrentValue = Mathf.Floor(scanPercentage);

        //if (base.chosenARPlane != null)
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
