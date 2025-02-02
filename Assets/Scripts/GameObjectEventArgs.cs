using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameObjectEventArgs : EventArgs {
    public GameObjectEventArgs(GameObject g)
    {
        this.gameObject = g;
    }
    public GameObject gameObject { get; set; }
}
