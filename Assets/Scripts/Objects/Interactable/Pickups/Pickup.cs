using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : InteractableObject
{
    void Update()
    {
        transform.Rotate(0, 36 * Time.deltaTime, 0, Space.World);
    }
}
