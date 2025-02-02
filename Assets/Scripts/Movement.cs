using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance;


    //public void MoveObject(Vector3 originalPos, Vector3 finalPos, float duration, ref GameObject objectToBeMoved)
    //{
    //    StartCoroutine(LerpMove(originalPos, finalPos, duration, objectToBeMoved));
    //}

    //IEnumerator LerpMove(Vector3 originalPos, Vector3 finalPos, float duration, ref GameObject objectToBeMoved)
    //{
    //    float elapsed = 0.0f;
    //    while (elapsed < duration)
    //    {
    //        objectToBeMoved.transform.position = Vector3.Lerp(originalPos, finalPos, elapsed / duration);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    objectToBeMoved.transform.position = finalPos;
    //    yield return null;

    //}
}
