using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSysAnimHook : MonoBehaviour
{
    public void Play()
    {
        GetComponent<ParticleSystem>().Play();
    }

    public void PlayAll()
    {
        foreach(var p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }
}
