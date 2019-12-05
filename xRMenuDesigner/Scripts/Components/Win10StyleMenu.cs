using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win10StyleMenu : MenuInterface
{
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

}
