using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    
    public void Feedback (){
        Application.OpenURL("mailto:josh@noyah.com");
    }
}
