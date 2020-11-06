using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMTurnCounter : MonoBehaviour
{
  public TextMeshProUGUI turnTextDisplay;
  public int currentTurn;

  private void Start()
  {
    SetTurn(currentTurn);
  }

  public void UpdateTurn(int direction)
  {
    currentTurn += direction;
    turnTextDisplay.SetText($"{currentTurn}");
  }

  public void SetTurn(int turn)
  {
    currentTurn = turn;
    turnTextDisplay.SetText($"{currentTurn}");
  }
}
