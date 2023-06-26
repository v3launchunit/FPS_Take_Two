using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class Volumetric : MonoBehaviour
{
    [SerializeField] private Color _tint = Color.white;

    private MeshRenderer _renderer;
    private Mesh         _mesh;
    private Light        _light;
    // private Vector3[]    _vertices;
    // private Color[]      _colors;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _mesh     = GetComponent<MeshFilter>().mesh;
        _light    = GetComponentInParent<Light>();

        // _vertices = _mesh.vertices;
        // _colors   = new Color[_vertices.Length];
    }

    void Update()
    {
        _renderer.material.color = _light.color * _tint;

        // for (int i = 0; i < _vertices.Length; i++ )
        // {   
        //     float c = Vector3.Distance(_vertices[i], _light.transform.position) / _light.range;
        //     _colors[i] = new Color(c, c, c);
        // }
        // _mesh.colors = _colors;
    }
}
