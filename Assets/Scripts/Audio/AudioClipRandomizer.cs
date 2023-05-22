using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipRandomizer : MonoBehaviour
{
    [SerializeField] private float           _pitchMin       = 0.9f;
    [SerializeField] private float           _pitchMax       = 1.1f;
    [SerializeField] private float           _altSoundChance = 0.01f;
    [SerializeField] private List<AudioClip> _altSoundList;

    void Start()
    {
        Randomize();
    }

    public void Randomize()
    {
        // var source   = GetComponent<AudioSource>();

        foreach (var source in GetComponents<AudioSource>())
        {
            source.pitch = Random.Range(_pitchMin, _pitchMax);

            if (_altSoundList.Count > 0 && Random.value <= _altSoundChance)
                source.clip = _altSoundList[Mathf.FloorToInt(Random.Range(0, _altSoundList.Count))];
        }
    }
}
