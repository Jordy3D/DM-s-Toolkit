using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderButton : MonoBehaviour
{
  public InputField loaderField;

  public ExternalLoader loader;

  public void LoadExternally()
  {
    loader.path = loaderField.text;
    loader.Load();
  }
}
