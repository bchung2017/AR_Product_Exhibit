using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{

    void Start(){
        var canvGroup = GetComponent<CanvasGroup>();
        FadeIN();
    }
    public float Duration = 0.7f;
    public void FadeIN(){
        var canvGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Fade(canvGroup, 0,1));
    }

    public void FadeOUT(){
        var canvGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Fade(canvGroup, 1,0));
    }

    public IEnumerator Fade(CanvasGroup canvasGroup, float start, float end){
        float counter = 0f;

        while(counter < Duration){
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }

}
