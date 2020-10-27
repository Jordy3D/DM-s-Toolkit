using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DMCharacterSpawner : MonoBehaviour
{
  public GameObject characterPrefab;

  public string incomingName;
  public Sprite incomingSprite;
  public int incomingSize;

  public TMP_InputField nameSelect;
  public TMP_Dropdown spriteSelect;
  public TMP_Dropdown sizeSelect;

  public DMCharacterMenu menu;

  public void SpawnCharacter()
  {
    GetIncomingTraits();

    DMCharacter newChar = Instantiate(characterPrefab).GetComponent<DMCharacter>();

    newChar.transform.position = Vector3.zero;

    newChar.characterName = incomingName;
    newChar.characterImage = incomingSprite;
    newChar.size = incomingSize;

    newChar.menu = menu;
  }

  public void GetIncomingTraits()
  {
    incomingName = nameSelect.text;
    //incomingSprite = spriteSelect.value;
    incomingSprite = spriteSelect.options[spriteSelect.value].image;
    incomingSize = sizeSelect.value + 1;
  }
}
