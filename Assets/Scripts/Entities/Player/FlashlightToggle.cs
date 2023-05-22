using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            GetComponent<AudioSource>().Play();
        }
    }
}
