using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputManager : MonoBehaviour
{
    [Header("Actions")]
    public SteamVR_Action_Boolean touch = null; // showing and hiding menu

    public SteamVR_Action_Boolean press = null;

    public SteamVR_Action_Vector2 touchPosition = null;

    public SteamVR_Action_Boolean mypress = null;


    [Header("Scene Objects")]
    public RadialMenu radialMenu = null;
    

    private void Awake()
    {
        touch.onChange += Touch;
        press.onStateUp += PressRelease;
        touchPosition.onAxis += Position;
    }

    private void OnDestroy()
    {
        touch.onChange -= Touch;
        press.onStateUp -= PressRelease;
        touchPosition.onAxis -= Position;
    }

    private void Position(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        print(axis);

        radialMenu.SetTouchPosition(axis);
    }

    private void Touch(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        print(newState);

        radialMenu.Show(newState);
    }

    private void PressRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Released");
        radialMenu.ActivateHighlightedSection();
    }

    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Press");
        radialMenu.ActivateHighlightedSection();
    }
}

