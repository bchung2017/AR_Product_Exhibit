using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Example1Controller : MonoBehaviour
{
    bool isMenuVisible = false;
    public Transform example1MenuTransform;
    public float duration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        HideMenu();
        isMenuVisible = false;
    }

    public void OnMenuToggleButtonPress()
    {
        if(isMenuVisible)
        {
            HideMenu();
            isMenuVisible = false;
        }
        else
        {
            ShowMenu();
            isMenuVisible = true;
        }
    }

    void ShowMenu()
    {
        example1MenuTransform.DOScale(Vector3.one, duration);
    }

    void HideMenu()
    {
        example1MenuTransform.DOScale(Vector3.zero, duration);
    }
}
