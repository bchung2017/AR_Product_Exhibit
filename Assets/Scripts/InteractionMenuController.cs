using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQuickLookPress()
    {
        SceneManager.LoadScene("QuickLookInteraction");
    }

    public void OnARMenuPress()
    {
        SceneManager.LoadScene("ARMenuInteraction");
    }

    public void OnTeleportPress()
    {
        SceneManager.LoadScene("TeleportInteraction");
    }
}
