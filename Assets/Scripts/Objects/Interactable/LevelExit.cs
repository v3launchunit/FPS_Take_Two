using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : InteractableObject
{
    [SerializeField] private string _nextLevel = "SampleScene";

    public override void OnInteract(GameObject other)
    {
        SceneManager.LoadScene(_nextLevel);
    }
}
