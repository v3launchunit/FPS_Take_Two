using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMat : LogicOutput
{
    [SerializeField] private Material _onMat;

    private Material _offMat;

    void Start()
    {
        _offMat = GetComponent<MeshRenderer>().material;
    }

    public override void ToggleActive(bool on)
    {
        _on = on;
        GetComponent<MeshRenderer>().material = (on) ? _onMat : _offMat;
    }
}
