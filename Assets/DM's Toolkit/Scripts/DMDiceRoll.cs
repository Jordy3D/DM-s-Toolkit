using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMDiceRoll : MonoBehaviour
{
  public int sideCount, diceCount, highestRolls, modCount;

  public TMP_InputField diceCountInput, sideCountInput, highCountInput, modCountInput;
  public TextMeshProUGUI rollOutput;

  public bool hasHistory = false;
  public TextMeshProUGUI rollHistoryPrefab;
  public Transform rollHistoryContainer;

  public void RollDice()
  {
    sideCount = sideCountInput.text == "" ? 20 : StrInt(sideCountInput.text);
    diceCount = diceCountInput.text == "" ? 1 : StrInt(diceCountInput.text);
    if (hasHistory)
      highestRolls = highCountInput.text == "" ? 0 : StrInt(highCountInput.text);
    else
      highestRolls = 0;
    modCount = modCountInput.text == "" ? 0 : StrInt(modCountInput.text);

    DiceRoller(sideCount, diceCount, highestRolls, modCount);
  }

  void DiceRoller(int sides = 20, int dice = 1, int high = 1, int mod = 0, bool stopDupes = false)
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
    {
      finalOutput += $"Rolled: {output}.";
      if (modCount != 0)
        finalOutput += $"\nWith modifier: {StrInt(output) + modCount}";
    }
    else
    {
      finalOutput += $"Rolled: {output} with a sum of: {rollSum}";
      if (modCount != 0)
        finalOutput += $"\nWith modifier: {rollSum + modCount}";
    }

    if (high > 2)
    {
      finalOutput += $"\n\nHighest Rolls: {highOutput}, with a sum of: {highRollSum}";
      if (modCount != 0)
        finalOutput += $"\nWith modifier: {highRollSum + modCount}";
    }
    else if (high == 1)
    {
      finalOutput += $"\n\nHighest Roll: {highOutput}";
      if (modCount != 0)
        finalOutput += $"\nWith modifier: {StrInt(highOutput) + modCount}";
    }
    rollOutput.text = finalOutput;

    if (hasHistory)
    {
      TextMeshProUGUI rollHistory = Instantiate(rollHistoryPrefab, rollHistoryContainer).GetComponent<TextMeshProUGUI>();
      rollHistory.SetText(finalOutput);
      rollHistory.transform.SetAsFirstSibling();
    }
  }

  bool ArrayContains(int[] array, int val) { return (Array.IndexOf(array, val)) > -1 ? true : false; }

  bool ListContains(List<int> array, int val) { return array.Contains(val); }

  int StrInt(string input) { return int.Parse(input); }
}