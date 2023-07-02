using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : Pickup
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private int        _startingMainAmmo;
    [SerializeField] private int        _startingAltAmmo;

    public override void OnInteract(GameObject other)
    {
        if (_interactSound != null)
            Instantiate(_interactSound, transform.position, transform.rotation);

        if (other.GetComponentInChildren<WeaponSelect>().AddWeapon(_weapon, _startingMainAmmo, _startingAltAmmo))
        {
            // GameObject v = GameObject.Find("Vignette");
            // v.GetComponent<Image>().color = _pickupFlashColor;
            // v.SetActive(true);
            FindFirstObjectByType<Flash>().DoFlash(_pickupFlashColor);
            FindFirstObjectByType<HudHandler>().Log(_pickupText);
            Destroy(gameObject);
        }
        else if (_pickupFailText != "")
            FindFirstObjectByType<HudHandler>().Log(_pickupFailText);
    }
}
