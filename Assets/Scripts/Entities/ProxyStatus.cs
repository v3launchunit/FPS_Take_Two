using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyStatus : Status
{
    [SerializeField] private Status _status;
    [SerializeField] private float  _damageMult = 1;

    private void Start()
    {
        if (_bloodSpray == null)
            _bloodSpray = _status.BloodSpray;
    }

    public override int Damage(int damage, float multiplier)
    {
        return _status.Damage(damage, _damageMult * multiplier);
    }
    
    public override bool Organic   { get => _status.Organic; }
}
