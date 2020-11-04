using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DMUIManager : MonoBehaviour
{
  public GameObject DMUI;

  public KeyCode menuKey = KeyCode.P;

  bool menuIsActive = false;

  public List<GameObject> menuPanels;

  [Header("Spawning Icons")]
  //public GameObject spawnDetailPanel;
  bool spawnDetailPanelIsActive = false;

  public TMP_Dropdown spriteSelectorDropdown;
  public LoadSprites spriteLoader;

  public GameObject spriteButton;
  public Transform iconButtonHolder;
  public GameObject iconSelectPanel;
  bool iconSelectPanelIsActive = false;
  public Tooltip tooltip;

  public DMCharacterSpawner spawner;

  [Header("Spawning Icons")]
  //public GameObject initiativeDetailPanel;
  bool initiativeDetailPanelIsActive = false;

  private void Start()
  {
    DMUI.SetActive(false);

    foreach (var sprite in spriteLoader.spritesLoaded)
    {
      //print("I'm trying...");
      DMSpawnerButton button = Instantiate(spriteButton, iconButtonHolder).GetComponent<DMSpawnerButton>();
      button.spawner = spawner;
      button.buttonImage.sprite = sprite;
      button.GetComponent<TooltipTarget>().tooltip = tooltip;
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

  public void TogglePanel(int panelID)
  {
    DisableAllOtherPanels(panelID);

    if (menuPanels[panelID].activeInHierarchy)
      menuPanels[panelID].SetActive(false);
    else
      menuPanels[panelID].SetActive(true);
  }

  public void ToggleSpawnDetails()
  {
    //spawnDetailPanelIsActive = !spawnDetailPanelIsActive;
    menuPanels[0].SetActive(spawnDetailPanelIsActive);
  }

  public void ToggleIconSelect()
  {
    iconSelectPanelIsActive = !iconSelectPanelIsActive;
    iconSelectPanel.SetActive(iconSelectPanelIsActive);
  }

  public void ToggleInitiativeDetails()
  {
    //initiativeDetailPanelIsActive = !initiativeDetailPanelIsActive;
    menuPanels[1].SetActive(initiativeDetailPanelIsActive);
  }

  public void DisableAllPanels()
  {
    foreach (var panel in menuPanels)
      SetPanelEnabled(panel, false);
  }

  public void DisableAllOtherPanels(int id)
  {
    for (int i = 0; i < menuPanels.Count; i++)
      if (i != id)
        menuPanels[i].SetActive(false);
  }

  void SetPanelEnabled(GameObject panel, bool state)
  {
    state = false;
    panel.SetActive(state);
  }
}
