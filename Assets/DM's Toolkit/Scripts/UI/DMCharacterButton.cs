using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DMCharacterButton : MonoBehaviour
{
  public DMCharacter character;

  public Transform statusHolder;

  public GameObject statusPrefab;

  public DMCharacterMenu menu;

  public void ToggleStatus(string status)
  {
    var curStatus = statusHolder?.Find(status);
    if (!curStatus)
    {
      TextMeshPro statusText = GameObject.Instantiate(statusPrefab, statusHolder).GetComponent<TextMeshPro>();
      statusText.name = status;
      statusText.SetText(status);
    }
    else
    {
      Destroy(curStatus.gameObject);
    }
  }

  void UpdateRange()
  {

  }

  public void ToggleName()
  {
    character.showName = !character.showName;
    character.UpdateName();
  }

  public void DeleteCharacter()
  {
    menu.transform.SetParent(null);
    menu.gameObject.SetActive(false);
    Destroy(character.gameObject);

    character = null;
  }
}
