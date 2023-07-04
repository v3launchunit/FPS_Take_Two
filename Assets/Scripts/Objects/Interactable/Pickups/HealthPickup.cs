using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : Pickup
{
    [SerializeField] private bool   _armorPickup = false;
    [SerializeField] private bool   _canOverheal = false;
    [SerializeField] private int    _amount      = 15;

    public override void OnInteract(GameObject other)
    {
        if 
        (
            (!_armorPickup && other.GetComponentInParent<Status>().Heal(_amount, _canOverheal)) 
            ||
            (_armorPickup  && other.GetComponentInParent<PlayerStatus>().AddArmor(_amount))
        )
        {
            if (_interactSound != null)
                Instantiate(_interactSound, transform.position, transform.rotation);
            
            FindFirstObjectByType<Flash>().DoFlash(_pickupFlashColor);
            FindFirstObjectByType<HudHandler>().Log(_pickupText);
            Destroy(gameObject);
        }
        else if (_pickupFailText != "")
            FindFirstObjectByType<HudHandler>().Log(_pickupFailText);
    }
}
