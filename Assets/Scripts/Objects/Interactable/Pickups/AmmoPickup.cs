using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickup : Pickup
{
    [SerializeField] private string[] _weapon;
    [SerializeField] private int[]    _amount;
    [SerializeField] private bool[]   _isAltAmmo;
    [SerializeField] private int[]    _sharedIndex;
    [SerializeField] private int[]    _sharedAmount;

    public override void OnInteract(GameObject other)
    {
        bool b = false;
        for (int i = 0; i < _weapon.Length; i++)
        {
            if (other.GetComponentInChildren<WeaponSelect>().AddAmmo(_weapon[i], _amount[i], _isAltAmmo[i]))
                b = true;
        }
        
        for (int i = 0; i < _sharedIndex.Length; i++)
        {
            if (other.GetComponentInChildren<WeaponSelect>().AddAmmo(_sharedIndex[i], _sharedAmount[i]))
                b = true;
        }

        if (b)
        {
            if (_interactSound != null)
                Instantiate(_interactSound, transform.position, transform.rotation);
            
            GameObject.FindObjectOfType<Flash>().DoFlash(_pickupFlashColor);
            GameObject.FindObjectOfType<HudHandler>().Log(_pickupText);
            Destroy(gameObject);
        }
        else if (_pickupFailText != "")
            GameObject.FindObjectOfType<HudHandler>().Log(_pickupFailText);
    }
}
