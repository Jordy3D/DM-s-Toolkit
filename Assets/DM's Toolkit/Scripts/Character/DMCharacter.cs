using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DMCharacter : MonoBehaviour, IPointerClickHandler
{
  public string characterName;
  public Sprite characterImage;
  public SpriteRenderer characterPortrait;

  public bool showName = true;
  public TextMeshPro characterNameText;

  public Transform statusHolder;
  public Transform menuHolder;

  #region Side
  [Header("Side")]
  public bool showSide = false;
  public SpriteRenderer sideDisplay;
  public Color sideColour = Color.red;
  #endregion

  #region Range
  [Header("Range")]
  public bool showRange;
  [Range(0, 5)]
  public float range = 1;
  public SpriteRenderer rangeDisplay;
  public Color rangeColour = Color.red;
  [Range(0, 1)]
  public float rangeAlpha = .3f;
  #endregion

  #region Size
  [Header("Size"), Range(1, 4)]
  public int size;
  #endregion

  #region Menu
  [Header("Menu")]
  public DMCharacterMenu menu;
  bool menuActive = false;
  public float menuOriginalScale = 0.005025044f;
  #endregion

  #region Misc
  public TextMeshPro cueIndicator, numberIndicator;
  public bool showCue, showNumber;
  #endregion

  #region Selected
  [Header("Selected")]
  public bool isSelected;
  public SpriteRenderer selectedDisplay;
  public Color selectedColor = Color.cyan;
  [Range(0, 1)]
  public float selectedAlpha = .3f;
  public DMSelection selection;
  #endregion

  Camera cam;
  private Plane dragPlane;
  private Vector3 offset;

  void Start()
  {
    cam = Camera.main;

    UpdateName();
    UpdatePortrait();
    UpdateSide();
    UpdateRange();
    UpdateSize();
    UpdateCue("ND");
    UpdateNumber(1);

    OnMouseUp();
  }

  void Update()
  {
    if (showName)
      UpdateName();

    if (showRange)
      UpdateRange();

    if (showSide)
      UpdateSide();
  }

  public void ShowMenu(bool _state)
  {
    menu.gameObject.SetActive(_state);
    menuActive = _state;
    menu.transform.localScale = (Vector3.one * 0.005025044f);

  }

  public void UpdateRange()
  {
    rangeDisplay.gameObject.SetActive(showRange);

    rangeColour.a = rangeAlpha;
    rangeDisplay.color = rangeColour;
    rangeDisplay.transform.localScale = Vector3.one * range;
  }
  public void UpdateSide()
  {
    sideDisplay.gameObject.SetActive(showRange);
    sideDisplay.color = sideColour;
  }
  public void UpdatePortrait()
  {
    characterPortrait.sprite = characterImage;
  }
  public void UpdateName()
  {
    characterNameText.gameObject.SetActive(showName);
    characterNameText.SetText(characterName);
  }
  public void UpdateSize()
  {
    transform.localScale = Vector3.one * size;
  }
  public void UpdateCue(string _cue)
  {
    //ND = No Damage (>76%HP)
    //SD = Slight Damage (<75% HP)
    //MD = Moderate Damage(< 50 % HP)
    //CD = Critical Damage(< 25 % HP)
    //TD = Terminal Damage(1 HP)
    cueIndicator.gameObject.SetActive(showCue);
    cueIndicator.SetText(_cue);
  }
  public void UpdateNumber(int _num)
  {
    numberIndicator.gameObject.SetActive(showNumber);
    numberIndicator.SetText(_num.ToString());
  }

  public void UpdateSelected(bool _state)
  {
    isSelected = _state;
    selectedDisplay.gameObject.SetActive(_state);

    selectedColor.a = selectedAlpha;
    selectedDisplay.color = selectedColor;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Right)
    {
      menu.character = this;
      menu.statusHolder = statusHolder;
      menu.menuHolder = menuHolder;
      ShowMenu(!menuActive);
    }
  }

  #region Drag
  void OnMouseDown()
  {
    dragPlane = new Plane(cam.transform.forward, transform.position);
    Ray camRay = cam.ScreenPointToRay(Input.mousePosition);

    float planeDist;
    dragPlane.Raycast(camRay, out planeDist);
    offset = transform.position - camRay.GetPoint(planeDist);

    //if (isSelected)
    //{
    //  selection.GetOffsetsFromSelected(GetComponent<DMSelectable>());
    //}
  }

  void OnMouseDrag()
  {
    Ray camRay = cam.ScreenPointToRay(Input.mousePosition);

    float planeDist;
    dragPlane.Raycast(camRay, out planeDist);
    transform.position = camRay.GetPoint(planeDist) + offset;
  }

  private void OnMouseUp()
  {
    transform.position = new Vector3(
      RoundedValue(transform.position.x, ((float)size / 4)),
      RoundedValue(transform.position.y, ((float)size / 4)),
      0);

    //print("Offset: " + ((float)size / 4) + " // Fraction: " + ((float)size * 2));
    //print("Rounded Value is " + RoundedValue(transform.position.x, ((float)size / 4)));
  }

  float RoundedValue(float input, float offset = .25f, float fraction = 2)
  {
    return ((Mathf.Round((input - offset) * fraction)) / fraction) + offset;
  }
  #endregion
}
