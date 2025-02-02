using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsScreenController : MonoBehaviour
{
    public Toggle toggle;
    public EventHandler IsToggleAutomaticMenuOn;
    public EventHandler IsToggleAutomaticMenuOff;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleAutomaticMenu()
    {
        EventArgs e = new EventArgs();
        if (toggle.isOn)
        {
            OnIsToggleAutomaticMenuOn(e);
        }
        else
        {
            OnIsToggleAutomaticMenuOff(e);
        }
    }

    protected virtual void OnIsToggleAutomaticMenuOn(EventArgs e)
    {
        EventHandler handler = IsToggleAutomaticMenuOn;
        handler?.Invoke(this, e);
    }

    protected virtual void OnIsToggleAutomaticMenuOff(EventArgs e)
    {
        EventHandler handler = IsToggleAutomaticMenuOff;
        handler?.Invoke(this, e);
    }
}
