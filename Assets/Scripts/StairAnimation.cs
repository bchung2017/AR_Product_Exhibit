using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairAnimation : Singleton<StairAnimation>
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveOnStairs(string direction, string rotation, GameObject g, float radius, Vector3 center, float duration, float height)
    {
        StartCoroutine(MoveOnStairs_Coroutine(direction, rotation, g, radius, center, duration, height));
    }

    IEnumerator MoveOnStairs_Coroutine(string direction, string rotation, GameObject g, float radius, Vector3 center, float duration, float height)
    {
        int d = 0, r = 0;

        if (direction == "up")
            d = 1;
        else if (direction == "down")
            d = -1;

        if (rotation == "clockwise")
            r = -1;
        else if (rotation == "counterclockwise")
            r = 1;

        // for (int deg = 0; Mathf.Abs(deg) < 360; deg += r)
        // {
        //     Vector3 originalPos = g.transform.position;
        //     Quaternion originalRot = g.transform.rotation;
        //     float xPos = radius * Mathf.Cos(deg * Mathf.PI / 180) + center.x;
        //     float zPos = radius * Mathf.Sin(deg * Mathf.PI / 180) + center.y;
        //     float yPos = Mathf.Lerp(originalPos.y, height, (float)deg / 360) * d;
        //     Vector3 finalPos = new Vector3(xPos, yPos, zPos);
        //     Quaternion finalRot = Quaternion.LookRotation(finalPos - originalPos, Vector3.up);
        //     float elapsedTime = 0;
        //     while (elapsedTime < duration)
        //     {
        //         g.transform.position = Vector3.Lerp(originalPos, finalPos, elapsedTime / duration);
        //         g.transform.rotation = Quaternion.Lerp(originalRot, finalRot, elapsedTime / duration);
        //         elapsedTime += Time.deltaTime;
        //         yield return null;
        //     }
        //     g.transform.position = finalPos;
        //     g.transform.rotation = finalRot;
        //     yield return null;
        // }

        // float originalXRot = g.transform.rotation.eulerAngles.x;
        // float finalXRot;
        // Debug.Log("xRot: " + originalXRot);
        // if (originalXRot > 180)
        //     finalXRot = 360;
        // else
        //     finalXRot = 0;
        // float finalelapsedTime = 0.0f;
        // while (finalelapsedTime < 0.3f)
        // {
        //     float xRotLerp = Mathf.Lerp(originalXRot, finalXRot, finalelapsedTime / 0.3f);
        //     Debug.Log("xRot: " + xRotLerp);
        //     g.transform.rotation = Quaternion.Euler(xRotLerp, g.transform.rotation.y, g.transform.rotation.z);
        //     finalelapsedTime += Time.deltaTime;
        //     yield return null;
        // }
        // g.transform.rotation = Quaternion.Euler(0, g.transform.rotation.y, g.transform.rotation.z);
        // yield return null;

        Vector3 originalPos = g.transform.position;
        float originalRot_y = g.transform.rotation.eulerAngles.y;
        Vector3 finalPos = originalPos + new Vector3(0, height, 0)*d;
        float finalRot_y = originalRot_y + 360.0f;

        for (int deg = 0; Mathf.Abs(deg) < 360; deg += r)
        {
            g.transform.position = Vector3.Lerp(originalPos, finalPos, Mathf.Abs(deg)/360f);
            float yRotation = Mathf.Lerp(originalRot_y, finalRot_y, Mathf.Abs(deg)/360f);
            g.transform.eulerAngles = new Vector3(g.transform.eulerAngles.x, yRotation, g.transform.rotation.z);
            yield return null;
        }

        g.transform.position = finalPos;
        g.transform.eulerAngles = new Vector3(g.transform.eulerAngles.x, finalRot_y, g.transform.rotation.z);
        yield return null;
    }
}
