using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public GameObject player;
    public Shader shaderDifuse;
    public Shader shaderTransparent;
    public float targetAlpha;
    public float time;
    
    public bool mustFadeBack = false;

    private bool collided;
    private List<Collider> hit = new List<Collider>();
    private List<GameObject> o = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shaderDifuse = Shader.Find("Diffuse");
        shaderTransparent = Shader.Find("Transparent/Diffuse");
    }

    // Update is called once per frame
    void Update()
    {
       
       // if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 60))
        //{
        if(collided)
        {
             mustFadeBack = true;

            for (int i = 0; i < hit.Count; i++)
            {
                if (hit[i].gameObject != o[i] && o[i] != null)
                {
                    FadeUp(o[i]);
                }
                    
                o[i] = hit[i].gameObject;
                Renderer _ro = o[i].GetComponent<Renderer>();
                if (_ro != null)
                {
                    if (_ro.material.shader != shaderTransparent)
                    {
                        o[i].GetComponent<Renderer>().material.shader = shaderTransparent;
                        Color k = o[i].GetComponent<Renderer>().material.color;
                        k.a = 0.5f;
                        o[i].GetComponent<Renderer>().material.color = k;
                    }
                }
                FadeDown(o[i]);
            }
        }
        else
        {
             if (mustFadeBack)
             {
                for (int i = 0; i < hit.Count; i++)
                {
                    mustFadeBack = false;
                    FadeUp(o[i]);
                }
             }
        }
    }

    void FadeUp(GameObject f)
    {
        //iTween.Stop(f);
        iTween.FadeTo(f, iTween.Hash("alpha", 1, "time", time, "oncomplete", "SetDifuseShading", "oncompletetarget", this.gameObject, "oncompleteparams", f));
    }

    void FadeDown(GameObject f)
    {
        //iTween.Stop(f);
        iTween.FadeTo(f, iTween.Hash("alpha", targetAlpha, "time", time));
    }

    void SetDifuseShading(GameObject f)
    {
        if (f.GetComponent<Renderer>().material.color.a == 1)
        {
            f.GetComponent<Renderer>().material.shader = shaderDifuse;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            FadeDown(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            FadeUp(other.gameObject);
        }
    }
}
