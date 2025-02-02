using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvironmentController : Singleton<EnvironmentController>
{
    public float height, pinchSpeed = 0.01f, multiplier = 1.0f;
    Camera cam;
    float movementSpeed;
    TouchController touchController;
    [SerializeField]
    GameObject centerPoint;
    public event EventHandler<GameObjectEventArgs> IsDoneMoving;
    public FixedJoystick joystick;


    private void Start()
    {
        gameObject.tag = "Environment";
        cam = Camera.main;
        touchController = cam.GetComponent<TouchController>();
        //touchController.OnPinch += PinchMove;

    }

    public void FixedUpdate()
    {
        Vector3 direction = (cam.transform.forward * joystick.Vertical + cam.transform.right * joystick.Horizontal)*multiplier * -1;
        direction = new Vector3(direction.x, 0 ,direction.z);
        //direction = direction.magnitude * cam.transform.forward;
        transform.position += direction;
    }
    public void Move(Vector3 location, float duration, GameObject destinationPOI)
    {
        Vector3 displacement = cam.transform.position - location;
        displacement.y = -1 * (location.y - height);
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = initialPosition + displacement;
        Debug.Log("finalPosition.y: " + finalPosition.y);
        StartCoroutine(LerpMove(initialPosition, finalPosition, duration, destinationPOI));
    }

    IEnumerator LerpMove(Vector3 originalPos, Vector3 finalPos, float duration, GameObject g)
    {
        Debug.Log("GameObject LerpMoving: " + gameObject.name);
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(originalPos, finalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = finalPos;
        GameObjectEventArgs e = new GameObjectEventArgs(g);
        DoneMoving(e);
        yield return null;
    }

    IEnumerator LerpStairMove()
    {
        yield return null;
    }

    void PinchMove(object sender, OnPinchEventArgs e)
    {
        Vector3 direction = cam.transform.forward;
        direction.y = 0;
        direction *= (e.scale * pinchSpeed);
        Debug.Log(gameObject.name + " direction: " + direction);
        transform.position -= direction;
    }

    public void StairMove(Directions direction)
    {
        if (direction == Directions.UP)
            StartCoroutine(StairMove_Coroutine("up", "clockwise", 1.0f, centerPoint.transform.position, 2.35f));
        if (direction == Directions.DOWN)
            StartCoroutine(StairMove_Coroutine("down", "counterclockwise", 1.0f, centerPoint.transform.position, 2.35f));
    }

    IEnumerator StairMove_Coroutine(string direction, string rotation, float duration, Vector3 center, float height)
    {
        int d = 0, r = 0;

        if (direction == "up")
            d = -1;
        else if (direction == "down")
            d = 1;

        if (rotation == "clockwise")
            r = -1;
        else if (rotation == "counterclockwise")
            r = 1;

        Vector3 originalPos = transform.position;
        float originalRot_y = transform.rotation.eulerAngles.y;
        Vector3 finalPos = originalPos + new Vector3(0, height, 0) * d;
        float finalRot_y = originalRot_y + 360.0f * r;

        for (int deg = 0; Mathf.Abs(deg) < 360; deg += r)
        {
            transform.position = Vector3.Lerp(originalPos, finalPos, Mathf.Abs(deg) / 360f);
            float yRotation = Mathf.Lerp(originalRot_y, finalRot_y, Mathf.Abs(deg) / 360f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.rotation.z);
            yield return null;
        }

        transform.position = finalPos;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, finalRot_y, transform.rotation.z);
        yield return null;
    }

    protected virtual void DoneMoving(GameObjectEventArgs e)
    {
        EventHandler<GameObjectEventArgs> handler = IsDoneMoving;
        handler?.Invoke(this, e);
    }


}
