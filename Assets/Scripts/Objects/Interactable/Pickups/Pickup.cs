using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : InteractableObject
{
    [SerializeField] protected string _pickupText;
    [SerializeField] protected string _pickupFailText;
    [SerializeField] protected Color  _pickupFlashColor = Color.white;

    void Update()
    {
        transform.Rotate(0, 36 * Time.deltaTime, 0, Space.World);
    }
}
