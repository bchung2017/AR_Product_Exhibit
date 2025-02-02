using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TouchController2 : MonoBehaviour
{
	public float speed;
	public Vector3 initialScale;
	public static Transform ScaleTransform;
	public GameObject portal;
	public event EventHandler<OnPinchEventArgs> OnPinch;

	// Start is called before the first frame update
	void Update()
    {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !IsPointerOverUIObject())
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			//Debug.Log("touchDeltaPosition: " + touchDeltaPosition);
			portal.transform.RotateAround(transform.position, Vector3.up, touchDeltaPosition.x * speed);
		}
	}



	protected virtual void OnIsPinching(OnPinchEventArgs e)
    {
		EventHandler<OnPinchEventArgs> handler = OnPinch;
		handler?.Invoke(this, e);
    }

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		eventData.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		return results.Count > 0;
	}
}
