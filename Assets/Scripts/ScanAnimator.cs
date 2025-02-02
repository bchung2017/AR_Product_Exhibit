using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanAnimator : MonoBehaviour
{
    private float speed = 300.0f;
    private bool scanAnimationComplete = true;

    void Start()
    {
        StartCoroutine("moveLeftDown");
    }
    void Update() {

    }

    void moveScanSprite()
    {

    }

    IEnumerator moveLeftDown()
    {
        Vector3 finalPos = new Vector3(gameObject.transform.position.x - 100, gameObject.transform.position.y-100, gameObject.transform.position.z);
        while(true)
        {
            while(Vector3.Distance(gameObject.transform.position, finalPos) > 1f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finalPos, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
            yield return StartCoroutine("moveRightDown");
        }
    }

    IEnumerator moveRightDown()
    {
        Vector3 finalPos = new Vector3(gameObject.transform.position.x + 100, gameObject.transform.position.y - 100, gameObject.transform.position.z);
        while(true)
        {
            while(Vector3.Distance(gameObject.transform.position, finalPos) > 1f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finalPos, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
            yield return StartCoroutine("moveRightUp");
        }
    }

    IEnumerator moveRightUp()
    {
        Vector3 finalPos = new Vector3(gameObject.transform.position.x + 100, gameObject.transform.position.y + 100, gameObject.transform.position.z);
        while(true)
        {
            while(Vector3.Distance(gameObject.transform.position, finalPos) > 1f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finalPos, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
            yield return StartCoroutine("moveLeftUp");
        }
    }

    IEnumerator moveLeftUp()
    {
        Vector3 finalPos = new Vector3(gameObject.transform.position.x - 100, gameObject.transform.position.y + 100, gameObject.transform.position.z);
        while(true)
        {
            while(Vector3.Distance(gameObject.transform.position, finalPos) > 1f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finalPos, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
            scanAnimationComplete = true;
            yield return StartCoroutine("moveLeftDown");
        }
    }
}
