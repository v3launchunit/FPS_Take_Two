using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : InteractableObject
{
    [SerializeField] protected string    _pickupText;
    [SerializeField] protected string    _pickupFailText;
    [SerializeField] protected Color     _pickupFlashColor = Color.white;
    [SerializeField] private   float     _pickupSpinSpeed     = 36;
    // [SerializeField] private   float     _pickupBobAmplitude  = 1;
    // [SerializeField] private   float     _pickupBobFrequency  = 1;
    // [SerializeField] private   LayerMask _pickupSnapTo;

    void Update()
    {
        if (_pickupSpinSpeed != 0)
            transform.Rotate(0, _pickupSpinSpeed * Time.deltaTime, 0, Space.World);

        // if (_pickupBobs)
        //     transform.localPosition

        // if 
        // (
        //     _pickupSnapTo != 0 &&
        //     Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1024, _pickupSnapTo)
        // )
        //     transform.position = _pickupBobAmplitude != 0 ? 
        //         hit.point + 
        //         (
        //             Vector3.up * 
        //             (
        //                 _pickupBobAmplitude * 
        //                 Mathf.Sin
        //                 (
        //                     Time.time * 
        //                     _pickupBobFrequency
        //                 )
        //             )
        //         ) : 
        //         hit.point;
    }
}
