using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool _pitch = true;
    [SerializeField] private bool _yaw   = true;
    [SerializeField] private bool _roll  = false;

    // Camera _mainCamera;
    // void Start() { _mainCamera = Camera.main; }

    void LateUpdate()
    {
        // transform.rotation = Camera.main.transform.rotation; 
        Vector3 eulerAngles = transform.eulerAngles;

        if (_pitch)
            eulerAngles.x = Camera.main.transform.eulerAngles.x;
        if (_yaw)
            eulerAngles.y = Camera.main.transform.eulerAngles.y;
        if (_roll)
            eulerAngles.z = Camera.main.transform.eulerAngles.z;

        transform.eulerAngles = eulerAngles;
    }
}
