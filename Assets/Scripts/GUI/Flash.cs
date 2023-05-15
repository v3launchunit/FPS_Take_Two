using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    private Image _image;
    private float _currentAlpha;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void DoFlash(Color color)
    {
        _image.color   = color;
        _currentAlpha  = color.a;
        _image.enabled = true;
    }

    void Update()
    {
        _currentAlpha -= Time.deltaTime;
        _image.color = new(_image.color.r, _image.color.g, _image.color.b, _currentAlpha);
        if (_currentAlpha >= 0)
            _image.enabled = true;
    }
}
