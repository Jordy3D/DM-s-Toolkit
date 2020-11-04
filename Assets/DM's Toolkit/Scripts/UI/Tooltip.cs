using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Tooltip : MonoBehaviour
{
  public TextMeshProUGUI textDisplay;
  public GameObject body;
  
  public void UpdateText(string _text)
  {
    textDisplay.SetText(_text);
  }
}
