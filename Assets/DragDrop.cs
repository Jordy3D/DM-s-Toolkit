using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour
{
  private Plane dragPlane;

  private Vector3 offset;

  private Camera myMainCamera;

  void Start()
  {
    myMainCamera = Camera.main;
  }

  void OnMouseDown()
  {
    dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
    Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

    float planeDist;
    dragPlane.Raycast(camRay, out planeDist);
    offset = transform.position - camRay.GetPoint(planeDist);
  }

  void OnMouseDrag()
  {
    Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

    float planeDist;
    dragPlane.Raycast(camRay, out planeDist);
    transform.position = camRay.GetPoint(planeDist) + offset;
  }

  private void OnMouseUp()
  {
    transform.position = new Vector3(RoundedValue(transform.position.x), RoundedValue(transform.position.y), 0);
  }

  float RoundedValue(float input, float offset = .25f, float fraction = 2)
  {
    return ((Mathf.Round((input - offset) * fraction)) / fraction) + offset;
  }
}