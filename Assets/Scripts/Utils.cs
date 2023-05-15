using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Transform FindRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if(child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = FindRecursive(child, childName);
                if (found != null)
                {
                        return found;
                }
            }
        }
        return null;
    }
}
