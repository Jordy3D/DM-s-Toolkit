using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Tooltip : MonoBehaviour
{
  public TextMeshProUGUI textDisplay;
  public GameObject body;

  public RectTransform tooltipPanel;

  public void UpdateText(string _text)
  {
    textDisplay.SetText(_text);
  }

  public void UpdateHeight(float _height)
  {
    tooltipPanel.sizeDelta = new Vector2(146.2f, _height);
  }

  public void UpdatePosition(float _posY)
  {
    tooltipPanel.anchoredPosition = new Vector2(-39, _posY);
  }
}
