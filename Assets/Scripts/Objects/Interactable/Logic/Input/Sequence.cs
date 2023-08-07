using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : LogicOutput
{
    [SerializeField] private int               _attachedSwitches;
    [SerializeField] private List<LogicOutput> _targets;

    public override void ToggleActive(bool on)
    {
        _attachedSwitches--;
        if (_attachedSwitches <= 0)
        {
            FindFirstObjectByType<HudHandler>().Log($"Sequence Complete!");

            foreach (var t in _targets)
            {
                t.ToggleActive(true);
            }
        }
        else
            FindFirstObjectByType<HudHandler>().Log($"Only {_attachedSwitches} more to go...");
    }
}
