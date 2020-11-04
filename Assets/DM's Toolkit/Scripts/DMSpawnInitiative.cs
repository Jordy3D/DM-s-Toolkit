using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DMSpawnInitiative : MonoBehaviour
{
  public TMP_InputField nameTextInput, initiativeTextInput;
  public Toggle enemyToggle;
  public Sprite incomingSprite;

  public GameObject playerInitiativePrefab, enemyInitiativePrefab;
  public Transform initiativeHolder;

  DMInitiative spawnedInit;

  public void SpawnInitiative()
  {
    if (enemyToggle.isOn)
      spawnedInit = Instantiate(enemyInitiativePrefab, initiativeHolder).GetComponent<DMInitiative>();
    else
      spawnedInit = Instantiate(playerInitiativePrefab, initiativeHolder).GetComponent<DMInitiative>();

    spawnedInit.nameText.text = nameTextInput.text;
    spawnedInit.initiativeText.text = initiativeTextInput.text;
    spawnedInit.name = $"{ nameTextInput.text}'s Initiative";
  }
}
