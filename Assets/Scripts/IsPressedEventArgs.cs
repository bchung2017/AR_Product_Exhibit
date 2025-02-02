using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsPressedEventArgs : EventArgs {
    public IsPressedEventArgs(ItemController i)
    {
        this.titleText = i.titleText;
        this.picture = i.image;
        this.gameObject = i.gameObject;
    }

    public IsPressedEventArgs(InsidePortalComponent p)
    {
        this.gameObject = p.gameObject;    
    }
    public string titleText { get; set; }
    public Sprite picture { get; set; }
    public GameObject gameObject { get; set; }
}
