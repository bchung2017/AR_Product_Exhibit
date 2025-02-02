using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeMainScreen : MonoBehaviour
{

    public Animator animator;
   

    // Start is called before the first frame update
    void Awake()
    {
        FadeIn();
    }

    public void FadeOut(){
        animator.Play("FadeOUT");
        Invoke("GoToTeleportScene", 1.0f);
        
    }

    public void FadeIn(){
        print("playing FadeIN Animation");
        animator.Play("FadeIN");
    }

    public void GoToTeleportScene(){
        SceneManager.LoadScene("TeleportInteraction");
    }
}
