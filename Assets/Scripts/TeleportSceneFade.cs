using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleportSceneFade : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
            FadeIn();
    }

    public void FadeOut(){
        animator.Play("FadeOUT");
        Invoke("GoToMainScene", 1.0f);
        
    }

    public void FadeIn(){
        animator.Play("FadeIN");
    }

    public void GoToMainScene(){
        SceneManager.LoadScene("MainScreen");
    }
}
