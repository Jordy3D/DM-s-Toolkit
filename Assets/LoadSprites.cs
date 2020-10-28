using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprites : MonoBehaviour
{
  public List<Sprite> spritesLoaded;
  public string[] foldersToLoadFrom;

  void Awake()
  {
    LoadIcons();
  }

  void LoadIcons()
  {
    Sprite[] icons;
    foreach (var folder in foldersToLoadFrom)
    {
      object[] loadedIcons = Resources.LoadAll(folder, typeof(Sprite));
      icons = new Sprite[loadedIcons.Length];
      //this
      for (int x = 0; x < loadedIcons.Length; x++)
      {
        spritesLoaded.Add((Sprite)loadedIcons[x]);
      }
    }
    //or this
    //loadedIcons.CopyTo (Icons,0);

  }
}
