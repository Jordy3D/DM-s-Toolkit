using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTarget : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
  public Sprite sprite;
  public Tooltip tooltip;

  bool tracking;

  void Update()
  {
    //if (!EventSystem.current.IsPointerOverGameObject())
    //{
    //  PointerEventData eventData = new PointerEventData(EventSystem.current);
    //  TooltipEnable(eventData);
    //}
    //if (IsPointerOverGameObject(this.gameObject))
    //{
    //  PointerEventData eventData = new PointerEventData(EventSystem.current);

    //  TooltipEnable(eventData);
    //}
    //else
    //{
    //  if (tooltip.body.activeInHierarchy)
    //    TooltipDisable();
    //}
  }

  public void TooltipEnable(PointerEventData eventData)
  {
    tooltip.body.SetActive(true);

    sprite = GetComponent<DMSpawnerButton>().buttonImage.sprite;
    tooltip.transform.position = transform.position;
    //print(eventData.position);
    tooltip.UpdateText(sprite.name);
  }

  public void TooltipDisable()
  {
    tooltip.body.SetActive(false);
  }



  public void OnPointerExit(PointerEventData eventData)
  {
    TooltipDisable();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    TooltipEnable(eventData);
  }
}
