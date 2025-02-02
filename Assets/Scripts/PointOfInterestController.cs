using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestController : MonoBehaviour
{
    protected GazeController gazeController;
    protected EnvironmentController environmentController;
    protected Camera cam;
    public float movementSpeed = 0.3f;
    protected GameObject selectedPoint;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        gazeController = Camera.main.GetComponent<GazeController>();
        gazeController.IsPressed += MoveTo;
        environmentController = GameObject.FindGameObjectWithTag("Environment").GetComponent<EnvironmentController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoveTo(object sender, IsPressedEventArgs e)
    {
        selectedPoint = e.gameObject;
        if (e.gameObject.GetInstanceID() == gameObject.GetInstanceID())
        {
            environmentController.Move(transform.position, movementSpeed, gameObject);
        }

    }

}
