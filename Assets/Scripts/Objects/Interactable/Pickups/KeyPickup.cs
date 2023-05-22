using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Pickup
{
    [SerializeField] private int _index;
    [SerializeField] private string _pickupText;
    [SerializeField] private string _pickupFailText;
    [SerializeField] private Color  _pickupFlashColor;

    public override void OnInteract(GameObject other)
    {
        if (_interactSound != null)
            Instantiate(_interactSound, transform.position, transform.rotation);
            
        if (other.GetComponentInParent<PlayerStatus>().AddKey(_index))
        {
            GameObject.FindObjectOfType<Flash>().DoFlash(_pickupFlashColor);
            GameObject.FindObjectOfType<HudHandler>().Log(_pickupText);
            Destroy(gameObject);
        }
        else if (_pickupFailText != "")
            GameObject.FindObjectOfType<HudHandler>().Log(_pickupFailText);
    }
}
