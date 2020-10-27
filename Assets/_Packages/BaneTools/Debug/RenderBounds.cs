using UnityEngine;

public class RenderBounds : MonoBehaviour
{
  private Renderer rend;

  public Color boundColor = Color.white;

  public bool wireCube;

  void OnDrawGizmos()
  {
    rend = GetComponent<Renderer>();

    Gizmos.color = boundColor;

    if (wireCube)
      Gizmos.DrawWireCube(rend.bounds.center, rend.bounds.size);
    else
      Gizmos.DrawCube(rend.bounds.center, rend.bounds.size);
  }
}
