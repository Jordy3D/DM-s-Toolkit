using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DMUIManager : MonoBehaviour
{
  public GameObject DMUI;

  public KeyCode menuKey = KeyCode.P;

  bool menuIsActive = false;

  public GameObject spawnDetailPanel;
  bool spawnDetailPanelIsActive = false;

  public TMP_Dropdown spriteSelectorDropdown;
  public LoadSprites spriteLoader;

  public GameObject spriteButton;
  public Transform iconButtonHolder;
  public GameObject iconSelectPanel;
  bool iconSelectPanelIsActive = false;

  public DMCharacterSpawner spawner;

  private void Start()
  {
    DMUI.SetActive(false);

    foreach (var sprite in spriteLoader.spritesLoaded)
    {
      //print("I'm trying...");
      DMSpawnerButton button = Instantiate(spriteButton, iconButtonHolder).GetComponent<DMSpawnerButton>();
      button.spawner = spawner;
      button.buttonImage.sprite = sprite;
    }
  }
  void Update()
  {
    if (Input.GetKeyDown(menuKey))
      ToggleMenu();
  }

  public void ToggleMenu()
  {
    menuIsActive = !menuIsActive;
    DMUI.SetActive(menuIsActive);
  }

  public void ToggleSpawnDetails()
  {
    spawnDetailPanelIsActive = !spawnDetailPanelIsActive;
    spawnDetailPanel.SetActive(spawnDetailPanelIsActive);
  }

  public void ToggleIconSelect()
  {
    iconSelectPanelIsActive = !iconSelectPanelIsActive;
    iconSelectPanel.SetActive(iconSelectPanelIsActive);
  }
}
