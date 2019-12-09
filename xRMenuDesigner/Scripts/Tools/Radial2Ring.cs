using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use RadialMenu to control RingMenu

public class Radial2Ring : MonoBehaviour
{
    public GameObject RingMenu = null;
    RingLayout Layout = null;

    public GameObject HMD_CAMERA;


    public void PressRight()
    {
        if (RingMenu != null)
        {
            RingLayout Layout = null;
            float step = 1.0f;
            int buttonNum = 0;

            var RingMenuTrans = RingMenu.transform;
            foreach (Transform currTrans in RingMenuTrans)
            {
                if (currTrans.tag == "ButtonPanel")
                {
                    Layout = currTrans.GetComponent<RingLayout>();
                    buttonNum = currTrans.childCount;
                }
            }

            if (Layout != null)
            {
                step = 360.0f / buttonNum;

                Layout.StartAngle = ((Layout.StartAngle + step) % 360.0f);
                Layout.CalculateRadial();
            }
        }
    }

    public void PressLeft()
    {
        if (RingMenu != null)
        {
            RingLayout Layout = null;
            float step = 1.0f;
            int buttonNum = 0;

            var RingMenuTrans = RingMenu.transform;
            foreach (Transform currTrans in RingMenuTrans)
            {
                if (currTrans.tag == "ButtonPanel")
                {
                    Layout = currTrans.GetComponent<RingLayout>();
                    buttonNum = currTrans.childCount;
                }
            }

            if (Layout != null)
            {
                step = 360.0f / buttonNum;

                Layout.StartAngle = ((Layout.StartAngle - step) % 360.0f);
                Layout.CalculateRadial();
            }
        }
    }

    public void PressTop() // Enable/Disable Ring menu
    {
        var RingMenuScript = RingMenu.GetComponent<RingMenu>();

        RingMenuScript.SwitchActive();
    }

    public void PressBottom() // Fix/Not Fix Ring Menu
    {
        var RingMenuScript = RingMenu.GetComponent<RingMenu>();

        RingMenuScript.SwitchRotateWithHMD();
    }

}
