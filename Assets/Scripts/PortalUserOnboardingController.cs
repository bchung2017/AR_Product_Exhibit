using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalUserOnboardingController : MonoBehaviour
{
    public string[] instructions;
    int instructionIndex;
    public TextMeshProUGUI instructionText, buttonText;
    // Start is called before the first frame update
    void Start()
    {
        instructionIndex = 0;
        instructionText.text = instructions[instructionIndex];
        instructionIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextButtonPress()
    {
        if(instructionIndex != instructions.Length)
        {
            instructionText.text = instructions[instructionIndex];
            instructionIndex++;
            if (instructionIndex == instructions.Length - 1)
                buttonText.text = "Close";
        }
        
        else
        {
            gameObject.SetActive(false);
        }
    }
}
