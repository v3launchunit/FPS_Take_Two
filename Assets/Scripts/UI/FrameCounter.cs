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
    private float           _peak    =    0;
    private float           _valley  = 1000;

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
        if (current < _valley)
            _valley = current;

        _framePanel.text = $"{1/average:0.000}/{average:000.0}"; //\n\n{_peak:000.0}/{_valley:000.0}
    }
}
