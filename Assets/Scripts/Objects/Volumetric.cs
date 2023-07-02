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

        _renderer.material.color = _light.color * _tint;

        // _vertices = _mesh.vertices;
        // _colors   = new Color[_vertices.Length];
    }
}
