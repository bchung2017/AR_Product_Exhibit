using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ARMenuController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText, ingredientsText;
    [SerializeField]
    private float hideDistance = 0.5f, hoverDistance = 0.3f, duration = 0.5f;
    GazeController gazeController;
    bool isShowing, isAnimating;
    GameObject selectedGameObject;
    SettingsScreenController settingsScreenController;

    // Start is called before the first frame update
    void Start()
    {
        gazeController = Camera.main.GetComponent<GazeController>();
        gazeController.IsPressed += ToggleScreen;
        gazeController.IsEmptyGaze += HideScreen;
        gazeController.IsNotEmptyGaze += ShowScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleScreen(object sender, IsPressedEventArgs e)
    {
        selectedGameObject = e.gameObject;
        if (!isAnimating)
        {
            if (!isShowing)
            {
                Vector3 originalPos = selectedGameObject.transform.position + new Vector3(0, hideDistance, 0);
                Vector3 finalPos = selectedGameObject.transform.position + new Vector3(0, hoverDistance, 0);
                StartCoroutine(AnimateScreen(originalPos, finalPos));
                isShowing = true;
            }
            else
            {
                Vector3 originalPos = selectedGameObject.transform.position + new Vector3(0, hideDistance, 0);
                Vector3 finalPos = selectedGameObject.transform.position + new Vector3(0, hoverDistance, 0);
                StartCoroutine(AnimateScreen(finalPos, originalPos));
                isShowing = false;
            }
        } 
    }

    void HideScreen(object sender, EventArgs e)
    {
        if (!isAnimating && isShowing)
        {
            Vector3 originalPos = selectedGameObject.transform.position + new Vector3(0, hideDistance, 0);
            Vector3 finalPos = selectedGameObject.transform.position + new Vector3(0, hoverDistance, 0);
            StartCoroutine(AnimateScreen(finalPos, originalPos));
            isShowing = false;
        }
    }

    void ShowScreen(object sender, IsPressedEventArgs e)
    {
        if(selectedGameObject != e.gameObject)
        {
            selectedGameObject = e.gameObject;
            isShowing = false;
        }
        
        if (!isAnimating && !isShowing)
        {
            Vector3 originalPos = selectedGameObject.transform.position + new Vector3(0, hideDistance, 0);
            Vector3 finalPos = selectedGameObject.transform.position + new Vector3(0, hoverDistance, 0);
            StartCoroutine(AnimateScreen(originalPos, finalPos));
            isShowing = true;
        }
    }

    IEnumerator AnimateScreen(Vector3 originalPos, Vector3 finalPos)
    {
        isAnimating = true;
        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            transform.position = Vector3.Lerp(originalPos, finalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = finalPos;
        isAnimating = false;
        yield return null;

    }
}
