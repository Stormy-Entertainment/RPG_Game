using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class k_winning : MonoBehaviour
{
    public GameObject boss;

    public void Win()
    {
        SceneManager.LoadScene(6);
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.GetComponent<EnemyStats>().m_Health <= 0)
        {
            Win();
        }
    }
}
