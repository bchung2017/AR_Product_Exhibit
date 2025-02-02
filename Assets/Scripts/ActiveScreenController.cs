using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnEnable()
    {
        CursorController.Instance.cursor = GameObject.Find("Cursor").GetComponent<Image>();
        CursorController.Instance.cursorOutline =  GameObject.Find("CursorOutline").GetComponent<Image>();
    }
}
