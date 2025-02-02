using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StairAnimation.Instance.MoveOnStairs("down", "counterclockwise", gameObject, 5.0f, Vector3.zero, 0.001f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
