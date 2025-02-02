using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchController : MonoBehaviour
{
	public float initialFingersDistance;
	public Vector3 initialScale;
	public static Transform ScaleTransform;
	public event EventHandler<OnPinchEventArgs> OnPinch;

	// Start is called before the first frame update
	void Update()
    {
		int fingersOnScreen = 0;

		foreach (Touch touch in Input.touches)
		{
			fingersOnScreen++; //Count fingers (or rather touches) on screen as you iterate through all screen touches.

			//You need two fingers on screen to pinch.
			if (fingersOnScreen == 2)
			{

				//First set the initial distance between fingers so you can compare.
				if (touch.phase == TouchPhase.Began)
				{
					initialFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
				}
				else
				{
					float currentFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
					float scaleFactor = currentFingersDistance - initialFingersDistance;
					//Debug.Log("scaleFactor: " + scaleFactor);
					OnPinchEventArgs e = new OnPinchEventArgs(scaleFactor);
					OnIsPinching(e);
					//transform.localScale = initialScale * scaleFactor;
					//ScaleTransform.localScale = initialScale * scaleFactor;
				}
			}
		}
	}

	protected virtual void OnIsPinching(OnPinchEventArgs e)
    {
		EventHandler<OnPinchEventArgs> handler = OnPinch;
		handler?.Invoke(this, e);
    }
}
