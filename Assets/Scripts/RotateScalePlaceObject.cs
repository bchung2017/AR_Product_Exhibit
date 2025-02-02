using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class RotateScalePlaceObject : MonoBehaviour
{
    public GameObject spawnedObject, prefabToPlaceUI, userOnboardingUI;
    GameObject spawnedObjectUI, trackables; //<><><Reference to ARFoundation Trackables>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    Vector3 oldPosition;
    float oldFingerDistance, speed = 0.1f, lastTouchTime;
    bool isPlaced = false, isDragging = false;
    public Transform cam;

    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        prefabToPlaceUI.SetActive(false);
        userOnboardingUI.SetActive(true);
    }

    void Update()
    {
        if (Input.touchCount > 0 && !IsPointerOverUIObject() && !isPlaced)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && m_RaycastManager.Raycast(Input.GetTouch(0).position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    lastTouchTime = Time.time;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    isDragging = true;
                    RotatePreviewPrefab();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (lastTouchTime > Time.time - 0.3f && !isDragging)
                        MovePrefab();
                    isDragging = false;
                }
            }
            else if (Input.touchCount == 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    oldFingerDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    ElevateAndScalePreviewPrefab();
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    private void MovePrefab()
    {
        Pose hitPose = s_Hits[0].pose;

        if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
        {
            //spawnedObject = Instantiate(prefabToPlace, hitPose.position, hitPose.rotation);
            spawnedObject.SetActive(true);
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;
            TurnTransparent();
            spawnedObject.transform.localScale = Vector3.one;
            spawnedObject.transform.LookAt(cam);
            Vector3 targetAngle = spawnedObject.transform.localEulerAngles;
            targetAngle.x = 0;
            targetAngle.z = 0;
            spawnedObject.transform.localEulerAngles = targetAngle;
            spawnedObject.transform.parent = transform.parent;
            if (spawnedObject.GetComponentInChildren<Canvas>() != null)
            {
                spawnedObjectUI = spawnedObject.GetComponentInChildren<Canvas>().gameObject;
                if (spawnedObjectUI != null)
                    spawnedObjectUI.SetActive(false);
            }
            m_NumberOfPlacedObjects++;
            prefabToPlaceUI.SetActive(true);
            userOnboardingUI.SetActive(false);
        }
        else
        {
            if (m_CanReposition)
            {
                spawnedObject.transform.position = new Vector3(hitPose.position.x, spawnedObject.transform.position.y, hitPose.position.z);
            }
        }

        if (onPlacedObject != null)
        {
            onPlacedObject();
        }
    }

    private void RotatePreviewPrefab()
    {
        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        Debug.Log("touchDeltaPosition: " + touchDeltaPosition);
        spawnedObject.transform.RotateAround(spawnedObject.transform.position, Vector3.up, touchDeltaPosition.x * speed);
    }

    private void ElevateAndScalePreviewPrefab()
    {
        Vector2 touchDeltaPositionSum = Input.GetTouch(0).deltaPosition + Input.GetTouch(1).deltaPosition;
        float distance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        float scaleFactor = distance / oldFingerDistance;
        Debug.Log("scale factor: " + scaleFactor);
        //Debug.Log("touchDeltaPosition: " + touchDeltaPosition);
        spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x, spawnedObject.transform.position.y + (touchDeltaPositionSum.y * speed * speed), spawnedObject.transform.position.z);
        spawnedObject.transform.localScale *= scaleFactor;
        oldFingerDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    }

    public void OnConfirmButtonPress()
    {
        isPlaced = true;
        prefabToPlaceUI.SetActive(false);
        if (spawnedObjectUI != null)
            spawnedObjectUI.SetActive(true);
        trackables
            = GameObject.Find("Trackables");
        if (trackables)
            trackables.SetActive(false);

        TurnOpaque();
    }

    void TurnTransparent()
    {
        var renderers = spawnedObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var material = ToFadeMode(renderer.material);
            if (material.HasProperty("_Color"))
            {
                Color temp = material.color;
                temp.a = 0.25f;
                material.color = temp;
            }
        }
    }

    void TurnOpaque()
    {
        var renderers = spawnedObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var material = ToOpaqueMode(renderer.material);
            if (material.HasProperty("_Color"))
            {
                Color temp = material.color;
                temp.a = 1.0f;
                material.color = temp;
            }
        }
    }

    public Material ToOpaqueMode(Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
        return material;
    }

    public Material ToFadeMode(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        return material;
    }
}
