using UnityEngine;

[AddComponentMenu("BaneTools/Modules/Disable on Start")]
public class DisableOnStart : MonoBehaviour
{
  public bool destroyScript = false;

  void Start()
  {
    gameObject.SetActive(false);
    if (destroyScript)
      Destroy(GetComponent<DisableOnStart>());
  }
}
