using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class k_Ending : MonoBehaviour
{
    public GameObject explosion;
    public GameObject sound1;
    public GameObject victory;

    public AudioClip victorySound;

    public TextMeshProUGUI textDisplay;
    public string sentence;
    private int index;
    public float typingSpeed;
    public GameObject nextButton;

    public AudioSource audio;
    public int sceneNumber;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    public void Bomb()
    {
        Debug.Log("B");
        explosion.SetActive(true);
    }

    public void BossDie()
    {
        sound1.SetActive(true);
    }

    public void WinText()
    {
        victory.SetActive(true);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            nextButton.SetActive(true);
        }
    }

    public void Credits()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
