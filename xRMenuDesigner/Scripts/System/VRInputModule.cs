using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera m_Camera;

    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        // reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2.0f, m_Camera.pixelHeight / 2.0f);

        // raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        // clear 
        m_RaycastResultCache.Clear();

        // hover
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        // press
        if (m_ClickAction.GetStateDown(m_TargetSource))
            ProcessPress(m_Data);

        // release
        if (m_ClickAction.GetStateUp(m_TargetSource))
            ProcessRelease(m_Data);
    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        // set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // check for object hit, get the down handler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        // if no down handler, try and get click handler
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        // execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // check if actual
        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        // clear selected gameobject
        eventSystem.SetSelectedGameObject(null);

        // reset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
