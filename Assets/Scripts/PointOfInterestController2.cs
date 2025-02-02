using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestController2 : MonoBehaviour
{
    protected EnvironmentController environmentController;
    protected Camera cam;
    public float movementSpeed = 0.3f;
    protected GameObject selectedPointOfInterest;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        CursorController.Instance.OnSelect += MoveTo;
        environmentController = GameObject.FindGameObjectWithTag("Environment").GetComponent<EnvironmentController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoveTo(object sender, RaycastEventArgs e)
    {
        selectedPointOfInterest = e.hit.collider.gameObject;
        if (selectedPointOfInterest.GetInstanceID() == gameObject.GetInstanceID())
        {
            environmentController.Move(transform.position, movementSpeed, gameObject);
        }

    }

}
