using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class k_winning : MonoBehaviour
{
    public void Win()
    {
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        FindObjectOfType<PauseUI>().DisableUIElement();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(7);
    }
}
