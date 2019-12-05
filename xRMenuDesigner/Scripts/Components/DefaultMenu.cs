using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultMenu : MenuInterface
{
    string TitleText;
    float ButtonHeight = 33.0f;

    public override void InitMenu(int _buttonNum, bool _isRootMenu)
    {
        ButtonNum = _buttonNum;

        if (!_isRootMenu)
        {
            _DisableSelf();
        }

        ResizeMenuHeight(ButtonNum);
    }

    public void SetTitle(string newTitle)
    {
        var trans = this.transform;
        foreach (Transform currTrans in trans)
        {
            if (currTrans.tag == "MenuTitle")
            {
                currTrans.GetComponent<Text>().text = newTitle;
                return;
            }
        }
        Debug.Log("Failed to change menu title!");
    }

    // Automatically update menu size based on number of buttons
    private void ResizeMenuHeight(int buttonNum)
    {
        if(buttonNum >= 1)
        {
            var rect = this.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + buttonNum * ButtonHeight);
        }
    }

    public void AddOneButton()
    {
        ButtonNum ++;
        var rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + ButtonHeight);
    }

    public void RemoveOneButton()
    {
        ButtonNum--;
        var rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - ButtonHeight);
    }
}
