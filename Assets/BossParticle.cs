using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle1;
    [SerializeField] private ParticleSystem particle2;
    [SerializeField] private ParticleSystem particle3;

    public void ActivateParticles()
    {
        particle1.Play();
        particle2.Play();
        particle3.Play();
    }
}
