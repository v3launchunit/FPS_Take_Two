using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : Pickup
{
    [SerializeField] private bool   _armorPickup = false;
    [SerializeField] private int    _amount      = 15;
    [SerializeField] private string _pickupText;
    [SerializeField] private string _pickupFailText;
    [SerializeField] private Color  _pickupFlashColor;

    public override void OnInteract(GameObject other)
    {
        if (_interactSound != null)
            Instantiate(_interactSound, transform.position, transform.rotation);
            
        if (other.GetComponentInParent<Status>().Heal(_amount))
        {
            GameObject.FindObjectOfType<Flash>().DoFlash(_pickupFlashColor);
            GameObject.FindObjectOfType<HudHandler>().Log(_pickupText);
            Destroy(gameObject);
        }
        else if (_pickupFailText != "")
            GameObject.FindObjectOfType<HudHandler>().Log(_pickupFailText);
    }
}
