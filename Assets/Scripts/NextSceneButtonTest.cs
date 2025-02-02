using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButtonTest : MonoBehaviour
{

    public void MarkerScene(){
        SceneManager.LoadScene("ARMarkersScene");
    }
    public void PortalScene(){
        SceneManager.LoadScene("TeleportInteraction");
    }
    

}
