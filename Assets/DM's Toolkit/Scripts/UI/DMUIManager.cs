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

  private void Start()
  {
    DMUI.SetActive(false);

    spriteSelectorDropdown.AddOptions(spriteLoader.spritesLoaded);
    foreach (var option in spriteSelectorDropdown.options)
    {
      option.text = option.image.name;
    }
  }
  void Update()
  {
    if (Input.GetKeyDown(menuKey))
    {
      ToggleMenu();
    }
  }

  void ToggleMenu()
  {
    menuIsActive = !menuIsActive;
    DMUI.SetActive(menuIsActive);
  }

  public void ToggleSpawnDetails()
  {
    spawnDetailPanelIsActive = !spawnDetailPanelIsActive;
    spawnDetailPanel.SetActive(spawnDetailPanelIsActive);
  }
}
