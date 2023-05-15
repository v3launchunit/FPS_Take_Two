using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOutput : MonoBehaviour
{
    protected bool _on = false;

    public         void ToggleActive()        { ToggleActive(!_on); }
    public virtual void ToggleActive(bool on) { _on = on; }

    public bool On { get => _on; } // set => _on = value;
}
