using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMDiceRoll : MonoBehaviour
{
  public int sideCount, diceCount, highestRolls;

  public TMP_InputField diceCountInput, sideCountInput, highCountInput;
  public TextMeshProUGUI rollOutput;

  public void RollDice()
  {
    sideCount = StrInt(sideCountInput.text == "" ? 20.ToString() : sideCountInput.text);
    diceCount = StrInt(diceCountInput.text == "" ? 1.ToString() : diceCountInput.text);
    highestRolls = StrInt(highCountInput.text == "" ? 0.ToString() : highCountInput.text);

    DiceRoller(sideCount, diceCount, highestRolls);
  }

  void DiceRoller(int sides = 20, int dice = 1, int high = 1, bool stopDupes = false)
  {
    List<int> highestRolls = new List<int>();

    string output = "";
    string highOutput = "";
    int rollSum = 0;
    int highRollSum = 0;

    if (high > dice)
      high = dice;

    if (dice != 0 && sides != 0 && sides <= 9999 && dice <= 30)
    {
      for (var i = 0; i < diceCount; i++)
      {
        int roll = (int)Mathf.Floor(UnityEngine.Random.Range(0, sideCount)) + 1;

        if (ListContains(highestRolls, roll) && stopDupes == true)
          i--;
        else
        {
          highestRolls.Add(roll);

          rollSum = rollSum + roll;

          if (i == 0)
            output += roll;
          else
            output = $"{output}, {roll}";
        }
      }

      if (high > 0 && (high <= dice))
      {
        highestRolls.Sort();
        highestRolls.Reverse();

        for (var o = 0; o < high; o++)
        {
          if (o == 0)
            highOutput += highestRolls[o];
          else
            highOutput = $"{highOutput}, {highestRolls[o]}";

          highRollSum = highRollSum + highestRolls[o];
        }
      }
    }

    string finalOutput = "";
    if (dice == 1)
      finalOutput += $"The die rolled was {output}.";
    else
      finalOutput += $"The dice rolled were {output} with a sum of {rollSum}";

    if (high > 2)
      finalOutput += $"\n\nYour highest rolls are {highOutput} with the sum of the highest rolls being {highRollSum}";
    else if (high == 1)
      finalOutput += $"Your highest roll was {highOutput}";

    rollOutput.text = finalOutput;
  }

  bool ArrayContains(int[] array, int val) { return (Array.IndexOf(array, val)) > -1 ? true : false; }

  bool ListContains(List<int> array, int val) { return array.Contains(val); }

  int StrInt(string input) { return int.Parse(input); }
}