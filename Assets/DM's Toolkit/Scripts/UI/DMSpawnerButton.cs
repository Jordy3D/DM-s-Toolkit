using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DMSpawnerButton : MonoBehaviour
{
  public DMCharacterSpawner spawner;

  public Image buttonImage;

  public void SetIncomingSprite()
  {
    spawner.incomingSpriteImage.sprite = buttonImage.sprite;
    spawner.incomingSprite = buttonImage.sprite;
  }
}
