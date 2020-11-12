using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMSearch : MonoBehaviour
{
  public TMP_InputField searchInput;
  public Transform searchElementHolder;

  public List<Transform> searchElements;
  public string searchString;

  public void UpdateSearch()
  {
    searchString = searchInput.text;

    if (searchElements.Count == 0) // If there's no search elements...
      foreach (Transform child in searchElementHolder) //Find all child elements of the holder...
        searchElements.Add(child); //And add the search elements.

    foreach (var element in searchElements)
    {
      element.gameObject.SetActive(true); //Enable the element

      if (!element.name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
        element.gameObject.SetActive(false); //If it doesn't contain the search term, disable it
    }
  }
}

public static class StringExtensions
{
  public static bool Contains(this string source, string toCheck, System.StringComparison comp)
  {
    return source?.IndexOf(toCheck, comp) >= 0;
  }
}
