using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Pickup
{
    [SerializeField] private int _index;

    public override void OnInteract(GameObject other)
    {
        if (_interactSound != null)
            Instantiate(_interactSound, transform.position, transform.rotation);
            
        if (other.GetComponentInParent<PlayerStatus>().AddKey(_index))
        {
            FindFirstObjectByType<Flash>().DoFlash(_pickupFlashColor);
            FindFirstObjectByType<HudHandler>().Log(_pickupText);
            Destroy(gameObject);
        }
        else if (_pickupFailText != "")
            FindFirstObjectByType<HudHandler>().Log(_pickupFailText);
    }
}
