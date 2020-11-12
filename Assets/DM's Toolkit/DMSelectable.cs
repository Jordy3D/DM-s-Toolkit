using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DMSelectable : MonoBehaviour
{
  public bool selected;

  public UnityEvent onSelect, onDeselect;

  public void ChangeSelected(bool _selected)
  {
    if (_selected)
      onSelect.Invoke();
    else
      onDeselect.Invoke();
  }
}
