using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BT;
using System;

public class DMSelection : MonoBehaviour
{
  //To determine if we are clicking with left mouse or holding down left mouse
  float delay = 0.3f;
  float clickTime = 0f;

  //The start and end coordinates of the square we are making
  public Vector3 squareStartPos;
  public Vector3 squareEndPos;

  //The selection squares 4 corner positions
  Vector3 TL, TR, BL, BR;

  public GameObject selectionBox;

  public List<DMSelectable> selectables;
  public bool isSelecting = false;

  public Vector3[] selectionOffsets;

  public bool hasCreatedSelection;

  void Start()
  {

  }

  void Update()
  {
    //Select one or several units by clicking or draging the mouse
    SelectUnits();
  }

  //Select units with click or by draging the mouse
  void SelectUnits()
  {
    //Are we clicking with left mouse or holding down left mouse
    bool isClicking = false;
    bool isHoldingDown = false;

    //Click the mouse button
    if (Input.GetMouseButtonDown(0))
    {
      clickTime = Time.time;

      //We dont yet know if we are drawing a square, but we need the first coordinate in case we do draw a square
      RaycastHit hit;
      //Fire ray from camera
      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
      {
        //The corner position of the square
        squareStartPos = hit.point;
      }

      if (!hasCreatedSelection)
      {
        selectables.Clear();
      }
    }

    //Release the mouse button
    if (Input.GetMouseButtonUp(0))
    {
      if (Time.time - clickTime <= delay)
        isClicking = true;

      isSelecting = false;
    }

    if (hasCreatedSelection && Input.GetMouseButtonUp(0))
    {
      if (selectables.Count > 1)
      {
        FitSelectionBoxToBounds();
      }
      else
      {
        ToggleSelectionBox(false);
      }
    }

    //Holding down the mouse button
    if (Input.GetMouseButton(0))
    {
      if (Time.time - clickTime > delay)
        isHoldingDown = true;
    }

    //Select one unit with left mouse and deselect all units with left mouse by clicking on what's not a unit
    if (isClicking)
    {
      ToggleSelectionBox(false);
      foreach (var selectable in selectables)
      {
        selectable.ChangeSelected(false);
      }
      selectables.Clear();

      isSelecting = false;
    }

    //Drag the mouse to select all units within the square
    if (isHoldingDown)
    {
      isSelecting = true;

      //Get the latest coordinate of the square
      squareEndPos = Input.mousePosition;

      //Display the selection with a GUI image
      DisplaySquare();
    }
  }

  void FitSelectionBoxToBounds()
  {
    DMSelectable[] boundObjects = selectables.ToArray();
    Bounds bounds = GetBounds(boundObjects);
    float boundsWidth = bounds.size.x;
    float boundsHeight = bounds.size.y;

    selectionBox.transform.localScale = new Vector3(boundsWidth, boundsHeight, 1);
    selectionBox.transform.position = bounds.center;
  }

  void DisplaySquare()
  {
    //The start position of the square is in 3d space, or the first coordinate will move
    //as we move the camera which is not what we want
    Vector3 squareStartScreen = Camera.main.WorldToScreenPoint(squareStartPos);

    squareStartScreen.z = 0f;

    //Get the middle position of the square
    Vector3 middle = (squareStartScreen + squareEndPos) / 2f;

    //Change the size of the square
    float sizeX = Mathf.Abs(squareStartScreen.x - squareEndPos.x);
    float sizeY = Mathf.Abs(squareStartScreen.y - squareEndPos.y);

    //The problem is that the corners in the 2d square is not the same as in 3d space
    //To get corners, we have to fire a ray from the screen
    //We have 2 of the corner positions, but we don't know which,  
    //so we can figure it out or fire 4 raycasts
    TL = new Vector3(middle.x - sizeX / 2f, middle.y + sizeY / 2f, 0f);
    TR = new Vector3(middle.x + sizeX / 2f, middle.y + sizeY / 2f, 0f);
    BL = new Vector3(middle.x - sizeX / 2f, middle.y - sizeY / 2f, 0f);
    BR = new Vector3(middle.x + sizeX / 2f, middle.y - sizeY / 2f, 0f);

    //From screen to world
    RaycastHit hit;
    int i = 0;
    //Fire ray from camera
    if (Physics.Raycast(Camera.main.ScreenPointToRay(TL), out hit, 200f))
    {
      TL = hit.point;
      i++;
    }
    if (Physics.Raycast(Camera.main.ScreenPointToRay(TR), out hit, 200f))
    {
      TR = hit.point;
      i++;
    }
    if (Physics.Raycast(Camera.main.ScreenPointToRay(BL), out hit, 200f))
    {
      BL = hit.point;
      i++;
    }
    if (Physics.Raycast(Camera.main.ScreenPointToRay(BR), out hit, 200f))
    {
      BR = hit.point;
      i++;
    }
    hasCreatedSelection = false;

    //We could find 4 points
    if (i == 4)
    {
      UpdateSelectionBox();
      if (selectables.Count > 0)
      {
        hasCreatedSelection = true;
      }
    }
  }

  void UpdateSelectionBox()
  {
    ToggleSelectionBox(true);
    Vector3 boxMix = TL.MidPoint(BR);

    selectionBox.transform.position = boxMix;
    selectionBox.transform.localScale = new Vector3(Vector3.Distance(TL, TR), Vector3.Distance(TL, BL), .1f);
  }

  void ToggleSelectionBox(bool state)
  {
    selectionBox.SetActive(state);
  }

  public void GetOffsetsFromSelected(DMSelectable selected)
  {
    selectionOffsets = new Vector3[selectables.Count];
    for (int i = 0; i < selectables.Count; i++)
    {
      selectionOffsets[i] = new Vector3(selected.transform.position.x - selectables[i].transform.position.x,
                                        selected.transform.position.y - selectables[i].transform.position.y );
    }
  }

  public static Bounds GetBounds(DMSelectable[] objs)
  {
    Bounds bounds = new Bounds();
    Collider2D[] renderers = new Collider2D[objs.Length];
    for (int i = 0; i < objs.Length; i++)
    {
      renderers[i] = objs[i].GetComponent<Collider2D>();
    }
    if (renderers.Length > 0)
    {
      //Find first enabled renderer to start encapsulate from it
      foreach (Collider2D renderer in renderers)
      {
        if (renderer.enabled)
        {
          bounds = renderer.bounds;
          break;
        }
      }

      //Encapsulate for all renderers
      foreach (Collider2D renderer in renderers)
      {
        if (renderer.enabled)
        {
          bounds.Encapsulate(renderer.bounds);
        }
      }
    }
    return bounds;
  }
}