using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueMan : MonoBehaviour
{
    //This is a simple dialogue system which using TextMeshPro for the UI text
    //Create a empty object and grab this script on that
    //Create a TextMeshPro UI text and button


    public TextMeshProUGUI textDisplay;
    public string[] sentence;
    private int index;
    public float typingSpeed;
    public GameObject nextButton;

    //other customer setting
    public GameObject restart;
    public GameObject image1;
    public GameObject image2;

    //Using Coroutine function
    void Start()
    {
        StartCoroutine(Type());
        restart.SetActive(false);
    }


    //showing the sentence like typing
    IEnumerator Type()
    {
        foreach (char letter in sentence[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //Showing the next sentence by clicking the next button
    //add index number and run Start() --> Typr()
    //if all sentence were read, no text will show
    //hide next button during the text is showing or no more sentence
    public void NextSentence()
    {
        nextButton.SetActive(false);

        if (index < sentence.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            nextButton.SetActive(false);
            restart.SetActive(true);
        }

    }

    //showing next button when the sentence is finished
    void Update()
    {
        if (textDisplay.text == sentence[index])
        {
            nextButton.SetActive(true);
        }




        //For custom image setting
        if (index == 0 || index == 1 || index == 4)
        {
            image1.SetActive(true);
        }
        else
        {
            image1.SetActive(false);
        }

        if (index == 2 || index == 3 || index == 5)
        {
            image2.SetActive(true);
        }
        else
        {
            image2.SetActive(false);
        }


    }

    public void Restart()
    {
        index = 0;
        Start();
    }


}
