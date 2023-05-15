using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class IndicatorLight : LogicOutput
{
    [SerializeField] private Color _onColor;

    private Color _offColor;

    void Start()
    {
        _offColor = GetComponent<Light>().color;
    }

    public override void ToggleActive(bool on)
    {
        _on = on;
        GetComponent<Light>().color = (on) ? _onColor : _offColor;
    }
}
