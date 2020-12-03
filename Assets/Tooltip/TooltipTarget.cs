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

  public void TooltipEnable(PointerEventData eventData)
  {
    tooltip.body.SetActive(true);

    sprite = GetComponent<DMSpawnerButton>().buttonImage.sprite;
    tooltip.transform.position = transform.position;
    //print(eventData.position);
    tooltip.UpdateText(sprite.name);

    tooltip.UpdateHeight(60); //Default = 43
    tooltip.UpdatePosition(50); //Default = 0
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
