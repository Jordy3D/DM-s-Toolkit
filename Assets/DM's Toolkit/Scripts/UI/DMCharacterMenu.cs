using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DMCharacterMenu : MonoBehaviour
{
  public DMCharacter character;
  public Transform menuHolder;
  public Transform statusHolder;

  public DMCharacterButton[] buttons;

  private void OnEnable()
  {
    buttons = GetComponentsInChildren<DMCharacterButton>();

    foreach (var button in buttons)
    {
      button.character = character;
      button.menu = this;

      if (button.hasImage)
      {
        button.statusHolder = statusHolder;

        button.DisableStatus();

        foreach (Transform status in statusHolder)
          if (button.name == status.name)
            button.EnableStatus();
      }
    }

    transform.SetParent(menuHolder);
    transform.localPosition = Vector3.zero;
  }
}
