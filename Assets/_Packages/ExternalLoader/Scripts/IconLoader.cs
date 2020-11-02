using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconLoader : MonoBehaviour
{
  public ExternalLoader loader;

  public GameObject iconButtonPrefab;
  public Transform iconButtonParent;

  void Start()
  {

  }

  void Update()
  {

  }

  public void SpawnIcons()
  {
    foreach (var sprite in loader.loadedSprites)
    {
      GameObject button = Instantiate(iconButtonPrefab, iconButtonParent);
      button.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sprite;
      button.name = sprite.name;
    }
  }
}
