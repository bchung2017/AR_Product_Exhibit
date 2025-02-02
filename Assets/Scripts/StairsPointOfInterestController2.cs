using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StairsPointOfInterestController2 : PointOfInterestController2
{
    public Directions direction;
    protected override void Start()
    {
        base.Start();
        environmentController.IsDoneMoving += MoveOnStairs;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    void MoveOnStairs(object sender, GameObjectEventArgs e)
    {
        if (e.gameObject.GetInstanceID() == gameObject.GetInstanceID())
        {
            environmentController.StairMove(direction);
            Debug.Log("red point clicked");
        }
    }
}
