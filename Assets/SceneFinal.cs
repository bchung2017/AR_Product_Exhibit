using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFinal : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    void OnEnable(){
        FadeOut();
    }

    public void FadeOut(){
        animator.Play("FadeOUT");
        Invoke("GoToTeleportScene", 1.0f);
        
    }

    public void GoToTeleportScene(){
        SceneManager.LoadScene("TeleportInteraction");
    }
}
