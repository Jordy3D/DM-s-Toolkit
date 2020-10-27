using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
  public Transform cam;

  [Header("Zoom")]
  public float defaultZoom = 60;
  public float minZoom = 15, maxZoom = 90;
  public float zoomSensitivity = 10;
  public bool invertZoom = false;
  [Header("Drag")]
  Vector3 dragOrigin;
  public float groundZ;

  Camera myCam;

  void Start()
  {
    cam = transform;
    myCam = cam.GetComponent<Camera>();
  }

  void Update()
  {
    float fov = myCam.fieldOfView;
    float scroll = Input.GetAxis("Mouse ScrollWheel");

    if (Input.GetKeyDown(KeyCode.Mouse2))
    {
      dragOrigin = GetWorldPos(groundZ);
    }

    if (Input.GetKey(KeyCode.Mouse2))
    {
      Vector3 direction = dragOrigin - GetWorldPos(groundZ);

      cam.position += direction;
    }

    if (scroll != 0)
    {
      fov += (invertZoom ? scroll : -scroll) * zoomSensitivity;
      fov = Mathf.Clamp(fov, minZoom, maxZoom);
      myCam.fieldOfView = fov;
    }
  }

  Vector3 GetWorldPos(float z)
  {
    Ray myRay = myCam.ScreenPointToRay(Input.mousePosition);

    Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
    float distance;
    ground.Raycast(myRay, out distance);

    return myRay.GetPoint(distance);
  }
}
