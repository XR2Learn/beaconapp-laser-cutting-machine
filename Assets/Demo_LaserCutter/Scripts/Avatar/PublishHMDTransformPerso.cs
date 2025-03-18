//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR;
using VMachina;
using XdeEngine.Core;

// This script publishes the Vive head and controller pose to a neutral transform (outside of network culler for network use)

// ReSharper disable once InconsistentNaming
public class PublishHMDTransformPerso : MonoBehaviour {
    
    [FormerlySerializedAs("HeadPublisher")]
    public Transform m_headPublisher;
    [FormerlySerializedAs("HeadSubscriber")] 
    public Transform m_headSubscriber;
    [FormerlySerializedAs("RightControllerPublisher")]
    public Transform m_rightControllerPublisher;
    [FormerlySerializedAs("RightControllerSubscriber")]
    public Transform m_rightControllerSubscriber;
    [FormerlySerializedAs("LeftControllerPublisher")]
    public Transform m_leftControllerPublisher;
    [FormerlySerializedAs("LeftControllerSubscriber")]
    public Transform m_leftControllerSubscriber;

    private SteamVR_Input_Sources source_handLeft = SteamVR_Input_Sources.LeftHand;
    private SteamVR_Input_Sources source_handRight = SteamVR_Input_Sources.RightHand;
    private SteamVR_Action_Boolean m_actionIsMenuButtonPressed;
    private SteamVR_Action_Boolean m_actionIsTrackPadPressed;
    private SteamVR_Action_Boolean m_actionIsTriggerPressed;
    private SteamVR_Action_Boolean m_actionIsGripPressed;
    private LaserPointerRaycast m_myLaser;
    
    [FormerlySerializedAs("Menu")]
    public VRMenuManager m_menu;

    private async Task RegisterSteamVREvents()
    {
        //Set actions
        m_actionIsGripPressed = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("interact", "isGripClicked", true, true);
        m_actionIsMenuButtonPressed = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("interact", "isMenuClicked", true, true);
        m_actionIsTrackPadPressed = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("interact", "isPadClicked", true, true);
        m_actionIsTriggerPressed = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("interact", "isTriggerClicked", true, true);

        //Controller events
        m_actionIsGripPressed[source_handRight].onStateDown += m_menu.MenuClicked;
        m_actionIsMenuButtonPressed[source_handRight].onStateDown += m_myLaser.TriggerDown;
        m_actionIsTrackPadPressed[source_handRight].onStateDown += m_menu.PadClicked;
        m_actionIsTrackPadPressed[source_handRight].onStateUp += m_menu.PadReleased;
        m_actionIsTriggerPressed[source_handRight].onStateDown += m_menu.TriggerClicked;
        m_actionIsTriggerPressed[source_handRight].onStateUp += m_menu.TriggerReleased;

        m_actionIsGripPressed[source_handLeft].onStateDown += m_menu.MenuClicked;
        m_actionIsMenuButtonPressed[source_handLeft].onStateDown += m_myLaser.TriggerDown;
        m_actionIsTrackPadPressed[source_handLeft].onStateDown += m_menu.PadClicked;
        m_actionIsTrackPadPressed[source_handLeft].onStateUp += m_menu.PadReleased;
        m_actionIsTriggerPressed[source_handLeft].onStateDown += m_menu.TriggerClicked;
        m_actionIsTriggerPressed[source_handLeft].onStateUp += m_menu.TriggerReleased;
    }

    void Start()
    {
        
        m_myLaser= GameObject.FindObjectOfType<LaserPointerRaycast>();
        StartCoroutine("SearchForXdeScene");
    }

    IEnumerator SearchForXdeScene()
    {
        bool found = false;
        while (!found)
        {
            XdeScene l_XdeScene = FindObjectOfType<XdeScene>();
            if (l_XdeScene != null)
            {
                l_XdeScene.OnServerConnection += RegisterSteamVREvents;
                found = true;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
    
    private void OnDestroy()
    {
        XdeScene l_XdeScene = FindObjectOfType<XdeScene>();
        if(l_XdeScene)
            l_XdeScene.OnServerConnection -= RegisterSteamVREvents;

        if (m_actionIsGripPressed != null)
        {
            m_actionIsGripPressed[source_handRight].onStateDown -= m_menu.MenuClicked;
            m_actionIsGripPressed[source_handLeft].onStateDown -= m_menu.MenuClicked;
        }
        if(m_actionIsMenuButtonPressed != null)
        {
            m_actionIsMenuButtonPressed[source_handRight].onStateDown -= m_menu.MenuClicked;
            m_actionIsMenuButtonPressed[source_handLeft].onStateDown -= m_menu.MenuClicked;
        }
        if(m_actionIsTrackPadPressed != null)
        {
            m_actionIsTrackPadPressed[source_handRight].onStateDown -= m_menu.PadClicked;
            m_actionIsTrackPadPressed[source_handRight].onStateUp -= m_menu.PadReleased;
            m_actionIsTrackPadPressed[source_handLeft].onStateDown -= m_menu.PadClicked;
            m_actionIsTrackPadPressed[source_handLeft].onStateUp -= m_menu.PadReleased;
        }
        if(m_actionIsTriggerPressed != null)
        {
            m_actionIsTriggerPressed[source_handRight].onStateDown -= m_menu.TriggerClicked;
            m_actionIsTriggerPressed[source_handRight].onStateUp -= m_menu.TriggerReleased;
            m_actionIsTriggerPressed[source_handLeft].onStateDown -= m_menu.TriggerClicked;
            m_actionIsTriggerPressed[source_handLeft].onStateUp -= m_menu.TriggerReleased;
        }
    }

    void Update () 
    {
        m_headSubscriber.position = m_headPublisher.position;
        m_headSubscriber.rotation = m_headPublisher.rotation;
        if(m_rightControllerPublisher && m_rightControllerSubscriber)
        {
          m_rightControllerSubscriber.position = m_rightControllerPublisher.position;
          m_rightControllerSubscriber.rotation = m_rightControllerPublisher.rotation;
        }
        if(m_leftControllerPublisher && m_leftControllerSubscriber)
        {
          m_leftControllerSubscriber.position = m_leftControllerPublisher.position;
          m_leftControllerSubscriber.rotation = m_leftControllerPublisher.rotation;
        }
    }
    
}
