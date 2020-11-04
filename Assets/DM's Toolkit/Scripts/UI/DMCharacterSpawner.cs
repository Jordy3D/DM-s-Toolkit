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

  public Image incomingSpriteImage;

  public TMP_InputField nameSelect;
  public TMP_Dropdown sizeSelect;

  public DMCharacterMenu menu;

  public Vector2 spawnOffset = new Vector2(100, 0);

  public void SpawnCharacter()
  {
    GetIncomingTraits();

    DMCharacter newChar = Instantiate(characterPrefab).GetComponent<DMCharacter>();

    newChar.transform.position = GetWorldPos(0);

    newChar.characterName = incomingName;
    newChar.characterImage = incomingSprite;
    newChar.size = incomingSize;

    newChar.menu = menu;
  }

  public void GetIncomingTraits()
  {
    incomingName = nameSelect.text == "" ? incomingSprite.name : nameSelect.text;
    incomingSize = sizeSelect.value + 1;
  }

  Vector3 GetWorldPos(float z)
  {
    Ray myRay = Camera.main.ScreenPointToRay(ScreenMid() - spawnOffset);

    Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
    float distance;
    ground.Raycast(myRay, out distance);

    return myRay.GetPoint(distance);
  }

  public Vector2 Mid() { return new Vector2(.5f, .5f); }
  public Vector2 ScreenMid() { return new Vector2(Screen.width / 2, Screen.height / 2); }
}
