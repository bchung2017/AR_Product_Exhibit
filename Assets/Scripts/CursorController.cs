using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RSG;

public class CursorController : Singleton<CursorController>
{
    GazeController2 gazeController;
    public Image cursor, cursorOutline;
    public EventHandler<RaycastEventArgs> OnSelect;
    float selectDurationTime = 1.0f, fadeDurationtime = 0.2f;
    bool isFilling, isFilled = false, isGazing = false, isDifferent = false;
    public bool isCloseEnough = false;
    public RaycastHit selectedRaycast;
    public Collider selectedCollider;
    public float distance = 3.0f;
    Promise cursorPromise;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gazeController = cam.GetComponent<GazeController2>();
        gazeController.OnColliderGaze += OnGazeStart;
        gazeController.OnEmptyGaze += OnGazeEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedRaycast.transform != null)
        {
            float distance = Mathf.Abs(Vector3.Distance(new Vector3(selectedRaycast.transform.position.x, 0, selectedRaycast.transform.position.z), new Vector3(cam.transform.position.x, 0, cam.transform.position.z)));
            if (distance <= this.distance)
                isCloseEnough = true;
            else
                isCloseEnough = false;
            if (selectedRaycast.collider.gameObject != ARMenuController2.Instance.selectedGameObject)
                isDifferent = true;
            else
                isDifferent = false;
            if (isCloseEnough && isDifferent && isGazing && !isFilling)
                FillCircle(true);
            else if ((!isCloseEnough || !isDifferent || !isGazing) && isFilling)
                FillCircle(false);
        }
    }



    void OnGazeStart(object sender, GazeEventArgs e)
    {
        selectedRaycast = e.hit;
        if (selectedRaycast.collider.gameObject.GetComponent<PedestalController>() != null || selectedRaycast.collider.gameObject.GetComponent<PointOfInterestController>())
        {
            isGazing = true;
        }
        // selectedRaycast = e.hit;
    }

    void OnGazeEnd(object sender, EventArgs e)
    {
        isGazing = false;
        // FillCircle(false);
        // if (ARMenuController2.Instance.selectedGameObject != selectedRaycast.collider.gameObject)
        //     selectedRaycast = new RaycastHit();
    }

    protected virtual void Select(RaycastEventArgs e)
    {
        Debug.Log("Select event invoked");
        EventHandler<RaycastEventArgs> handler = OnSelect;
        handler?.Invoke(this, e);
    }

    void FillCircle(bool isFilling)
    {
        if (!isFilled)
        {
            StopCoroutine("IE_FillCircle");
            this.isFilling = isFilling;
            StartCoroutine("IE_FillCircle");
        }
    }

    IEnumerator IE_FillCircle()
    {
        float elapsedTime = 0.0f;
        float currentFillVal = cursor.fillAmount;
        float finalFillVal;
        if (isFilling)
        {
            finalFillVal = 1.0f;
        }
        else
        {
            finalFillVal = 0.0f;
        }

        while (elapsedTime < selectDurationTime)
        {
            cursor.fillAmount = Mathf.Lerp(currentFillVal, finalFillVal, elapsedTime / selectDurationTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cursor.fillAmount = finalFillVal;
        if (isFilling)
            yield return StartCoroutine(EmptyCircle());
        yield return null;
    }

    IEnumerator EmptyCircle()
    {
        isFilled = true;
        RaycastEventArgs e = new RaycastEventArgs(selectedRaycast);
        Select(e);
        Debug.Log("emptying circle");
        Color c;
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDurationtime)
        {
            c = cursor.color;
            c.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDurationtime);
            cursor.color = c;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cursor.fillAmount = 0.0f;
        c = cursor.color;
        c.a = 1.0f;
        cursor.color = c;
        isFilled = false;
        yield return null;
    }

    public void ShowCursor()
    {
        cursor.gameObject.SetActive(true);
        cursorOutline.gameObject.SetActive(true);
    }

    public void HideCursor()
    {
        cursor.gameObject.SetActive(false);
        cursorOutline.gameObject.SetActive(false);
    }
}
