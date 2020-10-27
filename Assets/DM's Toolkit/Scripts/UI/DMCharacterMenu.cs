using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMCharacterMenu : MonoBehaviour
{
  public DMCharacter character;
  public Transform menuHolder;

  public DMCharacterButton[] buttons;
  private void OnEnable()
  {
    buttons = GetComponentsInChildren<DMCharacterButton>();
    foreach (var button in buttons)
    {
      button.character = character;
    }

    menuHolder = character.transform.Find("Menu");
    transform.SetParent(menuHolder);
    transform.localPosition = Vector3.zero;
  }

  private void Update()
  {
    
  }
}
