using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Example2Controller : MonoBehaviour
{
    bool isMenuVisible = false;
    public Transform example2MenuTransform;
    public List<Transform> textTransforms = new List<Transform>();
    public float duration = 0.2f, textDuration = 0.1f;
    Coroutine currentCoroutine;

    private void OnEnable()
    {
        HideMenu();
        isMenuVisible = false;
    }
    public void OnMenuToggleButtonPress()
    {
        if (isMenuVisible)
        {
            HideMenu();
            isMenuVisible = false;
        }
        else
        {
            ShowMenu();
            isMenuVisible = true;
        }
    }

    void ShowMenu()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ShowMenuIE());
    }

    IEnumerator ShowMenuIE()
    {
        Tween menuTween = example2MenuTransform.DOScaleY(1.0f, duration);
        yield return menuTween.WaitForCompletion();
        foreach(Transform t in textTransforms)
        {
            t.DOScaleX(1, textDuration);
        }
    }

    void HideMenu()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(HideMenuIE());
    }

    IEnumerator HideMenuIE()
    {
        foreach (Transform t in textTransforms)
        {
            t.DOScaleX(0, textDuration);
        }
        example2MenuTransform.DOScaleY(0.0f, duration);
        yield return null;
    }
}
