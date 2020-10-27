using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DMCharacterButton : MonoBehaviour
{
  public DMCharacter character;

  public Transform statusHolder;

  public GameObject statusPrefab;

  private void OnEnable()
  {
    statusHolder = character.transform.Find("StatusHolder");
  }

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
}
