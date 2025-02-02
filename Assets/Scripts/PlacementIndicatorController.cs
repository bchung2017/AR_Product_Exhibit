using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicatorController : MonoBehaviour
{
    [SerializeField]
    float animationDurationTime = 0.5f;
    [SerializeField]
    Vector3 originalScale = Vector3.one;
    bool isVisible;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        if(!isVisible)
        {
            StartCoroutine("AnimateShow");
            
        }
    }

    IEnumerator AnimateShow()
    {
        float elapsedTime = 0.0f;
        while(elapsedTime < animationDurationTime)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / animationDurationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isVisible = true;
        transform.localScale = originalScale;
        yield return null;
    }

    public void Hide()
    {
        if (isVisible)
        {
            StartCoroutine("AnimateHide");
            
        }
    }

    IEnumerator AnimateHide()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < animationDurationTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / animationDurationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isVisible = false;
        transform.localScale = Vector3.zero;
        yield return null;
    }
}
