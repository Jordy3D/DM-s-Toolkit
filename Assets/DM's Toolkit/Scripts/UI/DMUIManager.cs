using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMUIManager : MonoBehaviour
{
  public GameObject DMUI;

  public KeyCode menuKey = KeyCode.P;

  bool menuIsActive = false;

  public GameObject spawnDetailPanel;
  bool spawnDetailPanelIsActive = false;


  private void Start()
  {
    DMUI.SetActive(false);
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
