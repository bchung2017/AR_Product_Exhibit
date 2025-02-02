using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppBrowserTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InAppBrowser.OpenURL("https://www.noyah.com/cherry-cordial.html");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
