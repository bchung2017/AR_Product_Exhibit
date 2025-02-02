using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour
{
    public static bool isDevMode = false;
    public GameObject aRSessionOrigin;
    public GameObject editorCamera;
    public Canvas worldCanvas;
    public bool isRemoteActive;
    // Start is called before the first frame update
    void Awake()
    {
        if(Application.isEditor && !isRemoteActive)
        {
            isDevMode = true;
            aRSessionOrigin.SetActive(false);
            editorCamera.SetActive(true);
            if (worldCanvas != null)
                worldCanvas.worldCamera = editorCamera.GetComponent<Camera>();
        }
        else
        {
            aRSessionOrigin.SetActive(true);
            editorCamera.SetActive(false);
            if (worldCanvas != null)
                worldCanvas.worldCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
