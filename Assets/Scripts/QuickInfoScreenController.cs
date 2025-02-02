using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class QuickInfoScreenController : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject picture;
    GazeController gazeController;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        gazeController = Camera.main.GetComponent<GazeController>();
        gazeController.IsPressed += ToggleScreen;
        gazeController.IsEmptyGaze += HideScreen;
        gazeController.IsNotEmptyGaze += ShowScreen;

        
        //settingsScreenController.IsToggleAutomaticMenuOn += 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void ToggleScreen(object sender, IsPressedEventArgs e)
    {   
        if(!anim.GetBool("Show"))
        {
            anim.SetBool("Show", true);
            titleText.text = e.titleText;
            picture.GetComponent<Image>().sprite = e.picture;
        }
        else 
        {
            anim.SetBool("Show", false);
            Debug.Log("toggling screen...");
        }     
    }

    void HideScreen(object sender, EventArgs e)
    {
        anim.SetBool("Show", false);
        Debug.Log("hiding screen...");
    }

    void ShowScreen(object sender, IsPressedEventArgs e)
    {
        anim.SetBool("Show", true);
        titleText.text = e.titleText;
        picture.GetComponent<Image>().sprite = e.picture;
        Debug.Log("showing screen...");
    }

    


}
