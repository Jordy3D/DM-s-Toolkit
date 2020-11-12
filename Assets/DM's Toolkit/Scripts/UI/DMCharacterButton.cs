using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DMCharacterButton : MonoBehaviour
{
  public DMCharacter character;

  public Transform statusHolder;

  public GameObject statusPrefab;

  public DMCharacterMenu menu;

  public bool hasImage = false;
  public Image myImage;

  private void Start()
  {
    if (hasImage)
    {
      myImage = GetComponentsInChildren<Image>()[1];
      myImage.color = Color.gray;
    }
  }

  public void ToggleStatus()
  {
    var curStatus = statusHolder?.Find(transform.name);
    if (!curStatus)
    {
      TextMeshPro statusText = GameObject.Instantiate(statusPrefab, statusHolder).GetComponent<TextMeshPro>();
      statusText.name = transform.name;
      statusText.SetText(transform.name);
      myImage.color = Color.white;
    }
    else
    {
      Destroy(curStatus.gameObject);
      myImage.color = Color.gray;
    }
  }
  
  public void DisableStatus()
  {
    //CheckForImage();
    myImage.color = Color.gray;
  }

  void CheckForImage()
  {
    if (!hasImage)
      myImage = GetComponentsInChildren<Image>()[1];
  }

  public void EnableStatus()
  {
    myImage.color = Color.white;
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
