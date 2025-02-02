using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private GameObject InsidePortal, activeScreen, doorFrame, portalUserOnboardingInstructions, Stucco01, Concrete; //Both Stucco01 and concrete are getting in the way.
    public Transform placementTransform;


    // Start is called before the first frame update
    void Start()
    {
        if (EditorController.isDevMode)
            SetLayerRecursively(InsidePortal, 0);
        else
            SetLayerRecursively(InsidePortal, 8);
    }

    public void OnChildTriggerEnter(GameObject camera)
    {
        Vector3 camPositionInPortalSpace = placementTransform.InverseTransformPoint(camera.transform.position);
        //Debug.Log("distance: " + camPositionInPortalSpace.z);
        if (camPositionInPortalSpace.z > -0.5f)
        {
            if(!activeScreen.activeSelf)
            {
                activeScreen.SetActive(true);
                doorFrame.SetActive(false);
                portalUserOnboardingInstructions.SetActive(true);
                Stucco01.SetActive(true); // this eliminates glitch on the door when instanciated far. The Stucco walls get activated once inside the portal.
                                          //  Concrete.SetActive(true); // same as above
                SetLayerRecursively(InsidePortal, 0);
            }
        }
        //else if(camPositionInPortalSpace.z <= -0.5f)
        //{

        //    SetLayerRecursively(InsidePortal, 8);
        //}
    }

    public void OnChildTriggerExit(GameObject camera)
    {
        Vector3 camPositionInPortalSpace = placementTransform.InverseTransformPoint(camera.transform.position);
        if (activeScreen.activeSelf)
        {
            if (camPositionInPortalSpace.z <= -0.5f)
            {
                activeScreen.SetActive(false);
                SetLayerRecursively(InsidePortal, 8);
                doorFrame.SetActive(true);
                portalUserOnboardingInstructions.SetActive(false);
                Stucco01.SetActive(false);
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void SetPosition(Vector3 position)
    {
        Vector3 displacement = transform.position - placementTransform.position;
        transform.position = position + displacement;
    }
}
