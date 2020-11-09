using UnityEngine;
using UnityEngine.Events;

public class RTSSelectable : MonoBehaviour
{
  public bool selected;

  public UnityEvent onSelect, onDeselect;

  internal bool isSelected
  {
    get { return _isSelected; }
    set
    {
      _isSelected = value;
      selected = value;

      if (selected)
        onSelect.Invoke();
      else
        onDeselect.Invoke();
      
      ////Replace this with your custom code. What do you want to happen to a Selectable when it get's (de)selected?
      //Renderer r = GetComponentInChildren<Renderer>();
      //if (r != null)
      //  r.material.color = value ? Color.red : Color.white;
    }
  }

  private bool _isSelected;

  void OnEnable()
  {
    RTSSelection.selectables.Add(this);
  }
  void OnDisable()
  {
    RTSSelection.selectables.Remove(this);
  }

}