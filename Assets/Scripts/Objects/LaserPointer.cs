using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour
{
    [SerializeField] private bool _targetCrosshairs;

    private RaycastHit _raycastHit;
    private Vector3    _impactPoint;

    // Update is called once per frame
    void LateUpdate()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();

        Vector3 angle = transform.rotation * Vector3.forward;
        
        if (_targetCrosshairs)
            if (Physics.Raycast(Camera.main.transform.position, angle, out _raycastHit, 1024))
                _impactPoint = _raycastHit.point;
            else 
                _impactPoint = Camera.main.transform.position + 1024 * angle;
        else
            if (Physics.Raycast(transform.position, angle, out _raycastHit, 1024))
                _impactPoint = _raycastHit.point;
            else 
                _impactPoint = transform.position + 1024 * angle;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, _impactPoint);
    }
}
