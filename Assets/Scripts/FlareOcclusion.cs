using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(LensFlareComponentSRP))]
public class FlareOcclusion : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float     _fadeTime = 0.1f;

    private LensFlareComponentSRP _flare;
    private bool                  _isDirectional;
    private float                 _flareIntensity;
    private float                 _flareVel;

    void Start()
    {
        _flare          = GetComponent<LensFlareComponentSRP>();
        _flareIntensity = _flare.intensity;
        _isDirectional  = GetComponent<Light>().type == LightType.Directional;
    }

    void Update()
    {
        if 
        (
            (
                _isDirectional && 
                Physics.Raycast
                (
                    Camera.main.transform.position,
                    transform.rotation * Vector3.back,
                    Camera.main.farClipPlane,
                    _layerMask,
                    QueryTriggerInteraction.Ignore
                )
            ) 
            ||
            (
                !_isDirectional && 
                Physics.Linecast
                (
                    Camera.main.transform.position, 
                    transform.position, 
                    _layerMask, 
                    QueryTriggerInteraction.Ignore
                )
            )
        )
        {
            _flare.intensity = Mathf.SmoothDamp(_flare.intensity, 0, ref _flareVel, _fadeTime);
            // _flare.enabled = false;
            // print("Occluded");
        }
        else
        {    
            _flare.intensity = Mathf.SmoothDamp(_flare.intensity, _flareIntensity, ref _flareVel, _fadeTime);
            // _flare.enabled = true;
        }
    }
}
