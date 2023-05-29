using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour
{
    [SerializeField] private int _sampleCount = 3600;

    private TextMeshProUGUI _framePanel;
    private List<float>     _samples = new();
    private float           _peak;

    void Start()
    {
        _framePanel = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float current = (1f / Time.unscaledDeltaTime);

        _samples.Add(current);
        if (_samples.Count > _sampleCount)
            _samples.RemoveAt(0);

        float average = 0;
        foreach(float s in _samples)
        {
            average += s;
        }
        average /= _samples.Count;

        if (current > _peak)
            _peak = current;

        _framePanel.text = $"{current:000}/{average:000}";
    }
}
