using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : LogicOutput
{
    [SerializeField] private bool _oneWay;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }   
    }

    public override void ToggleActive(bool on)
    {
        _on = _oneWay ? true : on;

        print(_on);
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(_on);
        }
    }
}
