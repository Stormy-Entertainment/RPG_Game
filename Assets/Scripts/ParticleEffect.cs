using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(DelayedDestroy());   
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
