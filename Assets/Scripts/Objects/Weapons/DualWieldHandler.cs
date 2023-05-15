using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWieldHandler : WeaponBase
{
    // private WeaponHandler[] _weapons;
    [SerializeField] private WeaponHandler _left;
    [SerializeField] private WeaponHandler _right;

    void Awake()
    {
        // _left  = gameObject.GetComponentsInChildren<WeaponHandler>(true)[0];
        // _right = gameObject.GetComponentsInChildren<WeaponHandler>(true)[1];
    }

    public bool AddAmmo(int amount) { return AddAmmo(amount, false); }
    public override bool AddAmmo(int amount, bool addAltAmmo)
    {
        if (!addAltAmmo)
            return _left.AddAmmo(amount, false);
        else
            return _right.AddAmmo(amount, true);
    }
    
    public override int MainAmmo       { get => _left.MainAmmo; }
    public override int MainAmmoMax    { get => _left.MainAmmoMax; }
    public override int SharedMainAmmo { get => _left.SharedMainAmmo; }
    public override int AltAmmo        { get => _right.AltAmmo; }
    public override int AltAmmoMax     { get => _right.AltAmmoMax; }
    public override int SharedAltAmmo  { get => _right.SharedAltAmmo; }
}
