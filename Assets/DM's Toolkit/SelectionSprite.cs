using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSprite : MonoBehaviour
{
  public SelectionBox master;

  public void OnTriggerEnter2D(Collider2D other)
  {
    if (master.master.isSelecting)
    {
      DMSelectable selectable = other.GetComponent<DMSelectable>();
      if (selectable)
      {
        master.master.selectables.Add(selectable);
        selectable.ChangeSelected(true);
      }
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    if (master.master.isSelecting)
    {
      DMSelectable selectable = other.GetComponent<DMSelectable>();

      if (master.master.selectables.Contains(selectable))
      {
        master.master.selectables.Remove(selectable);
        selectable.ChangeSelected(false);
      }
    }

  }
}
