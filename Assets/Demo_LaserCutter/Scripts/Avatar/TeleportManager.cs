using System;
using UnityEngine;
using Valve.VR;
using VMachina;
using XdeNetwork;
using XdeEngine.Core;

public class TeleportManager : MonoBehaviour
{
  public GameObject leftPalm;
  public GameObject rightPalm;

  // a reference to the hand
  public SteamVR_Input_Sources handLeft;
  public SteamVR_Input_Sources handRight;

  private TeleporterCustom _teleporter = null;

  public SteamVR_Action_Boolean enableTeleportAction;
  public SteamVR_Action_Boolean triggerClicked; 

  private System.Diagnostics.Stopwatch vTimer;
  private XdeScene xdeScene;

  // Use this for initialization
  void Start()
  {
    xdeScene = FindObjectOfType<XdeScene>();
    
    enableTeleportAction.AddOnStateDownListener(EnableTeleportRight, handRight);
    enableTeleportAction.AddOnStateDownListener(EnableTeleportLeft, handLeft);
    triggerClicked.AddOnStateDownListener(TriggerClicked, handLeft);
    triggerClicked.AddOnStateDownListener(TriggerClicked, handRight);
    triggerClicked.UpdateValues();
  }

  private void OnDestroy()
  {
    enableTeleportAction.RemoveOnStateDownListener(EnableTeleportRight, handRight);
    enableTeleportAction.RemoveOnStateDownListener(EnableTeleportLeft, handLeft);
    triggerClicked.RemoveOnStateDownListener(TriggerClicked, handLeft);
    triggerClicked.RemoveOnStateDownListener(TriggerClicked, handRight);
  }

  public void TriggerClicked(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  {
    if (xdeScene.SimulationState == SIMULATION_STATE.ACTIVE && _teleporter)
    {
      _teleporter.Teleport();
    }
  }

  void OnTeleported()
  {
    if (_teleporter)
      DestroyImmediate(_teleporter);
  }

  public void EnableTeleportRight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  {
    if (_teleporter)
    {
      Destroy(_teleporter.gameObject);
      return;
    }

    GameObject teleportContainer = new GameObject();
    teleportContainer.name = "teleportContainer";
    teleportContainer.transform.parent = rightPalm.transform;

    teleportContainer.transform.localPosition = Vector3.zero;
    teleportContainer.transform.localRotation = Quaternion.identity * Quaternion.Euler(0f, 100f, 0f);
    _teleporter = teleportContainer.AddComponent<TeleporterCustom>();
    _teleporter.Teleported += OnTeleported;
  }

  public void EnableTeleportLeft(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  {

    if (_teleporter)
    {
      Destroy(_teleporter.gameObject);
      return;
    }

    GameObject teleportContainer = new GameObject();
    teleportContainer.name = "teleportContainer";
    teleportContainer.transform.parent = leftPalm.transform;

    teleportContainer.transform.localPosition = Vector3.zero;
    teleportContainer.transform.localRotation = Quaternion.identity * Quaternion.Euler(0, 100f, 0f);
    _teleporter = teleportContainer.AddComponent<TeleporterCustom>();
    _teleporter.Teleported += OnTeleported;
  }
}


