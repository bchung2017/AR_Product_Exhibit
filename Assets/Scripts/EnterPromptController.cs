using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterPromptController : MonoBehaviour
{
    TextMeshProUGUI prompt;
    private void Awake()
    {
        prompt = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        StartCoroutine("showAndHide");
    }

    IEnumerator showAndHide()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
        yield return null;
    }
}
