using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class RingButton : ButtonInterface
{
    // How much the button size will be enlarged when the pointer enters
    public float scaleX = 0.005f;
    public float scaleY = 0.005f;
    public float scaleZ = 0.005f;

    public override void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("PointerDown");
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("PointerUp");
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("PointerEnter");

        var _deltaScale = new Vector3(scaleX, scaleY, scaleZ);
        transform.localScale = Vector3.Slerp(transform.localScale, transform.localScale + _deltaScale, 0.5f);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("PointerExit");

        var _deltaScale = new Vector3(scaleX, scaleY, scaleZ);
        transform.localScale = Vector3.Slerp(transform.localScale, transform.localScale - _deltaScale, 0.5f);
    }

    public override void SetIcon(Sprite newIcon)
    {
        this.GetComponent<Image>().sprite = newIcon;
    }

}
