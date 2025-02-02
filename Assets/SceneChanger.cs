using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

void OnEnable(){
    animator.SetTrigger("FadeIn");
}

    public void FadeToLevel (int levelIndex){
        levelToLoad=levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete(){
        print("OnFadeComplete()......");
        SceneManager.LoadScene(levelToLoad);
    }

        public void FadeToNextLevel() {
        FadeToLevel (SceneManager.GetActiveScene().buildIndex +1);
    }

}
