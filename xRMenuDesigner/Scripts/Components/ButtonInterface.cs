using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonInterface : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // The reference of subMenu
    protected GameObject subMenu;

    // false: trigger function, true: trigger submenu
    protected bool allowSubMenu = false;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointerUp");
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("PointerEnter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("PointerExit");
    }


    public virtual void SetText(string newText)
    {
        var trans = this.transform;
        foreach (Transform currTrans in trans)
        {
            if (currTrans.tag == "ButtonText")
            {
                currTrans.GetComponent<Text>().text = newText;
            }
        }
    }

    public virtual void SetIcon(Sprite newIcon)
    {
        var trans = this.transform;
        foreach (Transform currTrans in trans)
        {
            if (currTrans.tag == "ButtonIcon")
            {
                currTrans.GetComponent<Image>().sprite = newIcon;
            }
        }
    }

    public virtual void SetSubMenuRef(GameObject newSubMenu)
    {
        if (newSubMenu != null)
            SetAllowSubMenu(true);
        subMenu = newSubMenu;
    }

    public void EnableSubMenu()
    {
        if (allowSubMenu && subMenu != null)
        {
            // disable all sub menus triggered by buttons
            var currButtonPanel = this.transform.parent;
            currButtonPanel.BroadcastMessage("DisableSubMenu");

            // set sub menu active
            subMenu.SetActive(true);
        }
    }

    public void DisableSubMenu()
    {
        if (allowSubMenu && subMenu != null)
        {
            subMenu.SetActive(false);
        }
    }


    public void SetAllowSubMenu(bool status)
    {
        allowSubMenu = status;
    }

}
