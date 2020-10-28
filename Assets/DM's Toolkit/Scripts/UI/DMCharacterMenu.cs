using System.Collections;
using System.Collections.Generic;
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
      button.statusHolder = statusHolder;
    }

    transform.SetParent(menuHolder);
    transform.localPosition = Vector3.zero;
  }
}
