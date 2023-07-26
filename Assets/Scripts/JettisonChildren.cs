using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JettisonChildren : MonoBehaviour
{
    void Start()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())   
        {
            t.SetParent(transform.parent);
        }
    }
}
