using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class ARMenuController2 : Singleton<ARMenuController2>
{
    [SerializeField]
    private float hideDistance = 5f, hoverDistance = 1.5f, duration = 0.3f;
    public TextMeshProUGUI title;
    public bool isShowing, isAnimating;
    public GameObject selectedGameObject = null;
    Canvas canvas;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = cam;
        CursorController.Instance.OnSelect += ShowScreen;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = transform.position - cam.transform.position;
        transform.rotation = Quaternion.LookRotation(displacement, Vector3.up);
        if (isShowing && !CursorController.Instance.isCloseEnough)
        {
            HideScreen();
        }
    }

    void HideScreen(object sender = null, EventArgs e = null)
    {
        if (!isAnimating)
        {
            // CursorController.Instance.selectedRaycast = new RaycastHit();
            Vector3 originalPos = selectedGameObject.transform.position + new Vector3(0, hideDistance, 0);
            Vector3 finalPos = selectedGameObject.transform.position + new Vector3(0, hoverDistance, 0);
            selectedGameObject = null;
            StartCoroutine(AnimateScreen(finalPos, originalPos));
            isShowing = false;
        }
    }

    void ShowScreen(object sender, RaycastEventArgs e)
    {
        selectedGameObject = e.hit.collider.gameObject;
        if(selectedGameObject.GetComponent<PedestalController>() != null)
        {
            title.text = selectedGameObject.GetComponentInChildren<ChapstickController>().flavor;
            if (!isAnimating)
            {
                Vector3 originalPos = selectedGameObject.transform.position + new Vector3(0, hideDistance, 0);
                Vector3 finalPos = selectedGameObject.transform.position + new Vector3(0, hoverDistance, 0);
                StartCoroutine(AnimateScreen(originalPos, finalPos));
                isShowing = true;
            }
        }
    }

    IEnumerator AnimateScreen(Vector3 originalPos, Vector3 finalPos)
    {
        isAnimating = true;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(originalPos, finalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = finalPos;
        isAnimating = false;
        yield return null;

    }

    public void OnBackButtonPress()
    {
        HideScreen();
    }
}
