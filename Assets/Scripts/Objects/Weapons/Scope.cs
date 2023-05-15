using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] private float _scopeStrength = 2;
    [SerializeField] private float _scopeTime     = 0.1f;

    private bool  _scoped   = false;
    private float _scopeVel = 0;

    void Update()
    {
        float     fov  = Camera.main.fieldOfView;
        MouseLook look = Camera.main.GetComponent<MouseLook>();
        float fovTarget;
        if (_scoped)
        {
            fovTarget            = look.FovDesired / _scopeStrength;
            look.SensitivityMult =              1 / _scopeStrength;
        }
        else
        {
            fovTarget            = look.FovDesired;
            look.SensitivityMult = 1;
        }
        Camera.main.fieldOfView = Mathf.SmoothDamp(fov, fovTarget, ref _scopeVel, _scopeTime);

        if (Input.GetButtonDown("Fire2"))
            _scoped = !_scoped;
    }

    void OnDisable()
    {
        _scoped                                               = false;
        Camera.main.fieldOfView                               = Camera.main.GetComponent<MouseLook>().FovDesired;
        Camera.main.GetComponent<MouseLook>().SensitivityMult = 1;
    }
}
