using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMInitiative : MonoBehaviour
{
  public int initiative;
  public float currentTurnOffset = 25f;

  public string charName;

  public RectTransform body;

  public TextMeshProUGUI nameText, initiativeText;

  public bool currentTurn = false;

  private void Update()
  {
    SetCurrentTurn(currentTurn);
  }

  public void SetCurrentTurn(bool state)
  {
    if (state)
      body.localPosition = new Vector3(currentTurnOffset, 0, 0);
    else
      body.localPosition = Vector3.zero;
  }
}
