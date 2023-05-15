using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LinePointAnimHook : MonoBehaviour
{
    [SerializeField] private int     _linePointIndex;
    [SerializeField] private Vector3 _linePointPos;

    // Update is called once per frame
    void Update()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        
        line.SetPosition(_linePointIndex, _linePointPos);
    }
}
