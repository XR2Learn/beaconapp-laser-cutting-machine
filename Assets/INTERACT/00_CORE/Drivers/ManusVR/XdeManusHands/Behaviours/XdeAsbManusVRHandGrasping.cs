using UnityEngine;
using UnityEngine.EventSystems;
using ManusVR.Core.Apollo;
using System;

namespace XdeEngine.Assembly
{
  public class XdeAsbManusVRHandGrasping : MonoBehaviour
  {
    [Range(0, 60)]
    public int closeThreshold = 25;
    public EventTrigger.TriggerEvent onRightHandOpened, onRightHandClosed;
    public EventTrigger.TriggerEvent onLeftHandOpened, onLeftHandClosed;
    public float echoTime = 0.5f; //Time during the closed hand trigger will be call each frame

    private bool handTriggerRightClosed = false, indexTriggerRightClosed = false, handTriggerLeftClosed = false, indexTriggerLeftClosed = false;
    private float echoStartRight = -1, echoStartLeft = -1;

    [HideInInspector]
    public int listenerIdAttachR = -1, listenerIdDetachR = -1, listenerIdAttachL = -1, listenerIdDetachL = -1;

    private Apollo apollo;
    private ApolloRawData leftHandRawData;
    private ApolloRawData rightHandRawData;

    void Start()
    {
      // Register delegates for incoming handData
      apollo = Apollo.GetInstance(false);
      apollo.RegisterDataListener(NewRawData);
    }

    void OnDestroy()
    {
      if (apollo != null)
        apollo.UnRegisterDataListener(NewRawData);
    }

    public void NewRawData(ApolloRawData data, GloveLaterality side)
    {
      switch (side)
      {
        case GloveLaterality.GLOVE_LEFT:
          // store the raw data
          leftHandRawData = data;
          break;
        case GloveLaterality.GLOVE_RIGHT:
          // store the raw data
          rightHandRawData = data;
          break;
      }
    }

    void Update()
    {
      //right hand close/open event
      int rightCloseValue = 0;
      rightCloseValue = (int)GetCloseValue(device_type_t.GLOVE_RIGHT);
      if (rightCloseValue >= closeThreshold && !handTriggerRightClosed)
      {
        echoStartRight = Time.time;
        if (echoTime <= 0)
          onRightHandClosed.Invoke(new BaseEventData(EventSystem.current));
        handTriggerRightClosed = true;
      }
      else if (rightCloseValue < closeThreshold && handTriggerRightClosed)
      {
        BaseEventData eventData = new BaseEventData(EventSystem.current);
        onRightHandOpened.Invoke(eventData);
        echoStartRight = 0;
        handTriggerRightClosed = false;
      }

      //right hand close event echo
      if (echoStartRight > 0 && Time.time - echoStartRight < echoTime)
        onRightHandClosed.Invoke(new BaseEventData(EventSystem.current));
      else
        echoStartRight = 0;

      //left hand close/open event
      int leftCloseValue = 0;
      leftCloseValue = (int)GetCloseValue(device_type_t.GLOVE_LEFT);
      if (leftCloseValue >= closeThreshold && !handTriggerLeftClosed)
      {
        echoStartLeft = Time.time;
        if (echoTime <= 0)
          onLeftHandClosed.Invoke(new BaseEventData(EventSystem.current));

        handTriggerLeftClosed = true;
      }
      else if (leftCloseValue < closeThreshold && handTriggerLeftClosed)
      {
        BaseEventData eventData = new BaseEventData(EventSystem.current);
        onLeftHandOpened.Invoke(eventData);
        echoStartLeft = 0;
        handTriggerLeftClosed = false;
      }

      if (echoStartLeft > 0 && Time.time - echoStartLeft < echoTime)
        onLeftHandClosed.Invoke(new BaseEventData(EventSystem.current));
      else
        echoStartLeft = 0;
    }

    public enum CloseValue
    {
      Fist = 65,
      Small = 30,
      Tiny = 15,
      Open = 5
    }

    CloseValue GetCloseValue(device_type_t deviceType)
    {
      double averageSensorValue = 0;
      switch (deviceType)
      {
        default:
        case device_type_t.GLOVE_RIGHT:
          averageSensorValue = TotalAverageValue(rightHandRawData);
          break;
        case device_type_t.GLOVE_LEFT:
          averageSensorValue = TotalAverageValue(leftHandRawData);
          break;
      }

      CloseValue closest = CloseValue.Open;
      // Get the current close value
      foreach (CloseValue item in Enum.GetValues(typeof(CloseValue)))
      {
        // Div by 100.0 is used because an enum can only contain ints
        if (averageSensorValue > (double)item / 100.0)
          closest = item;
      }
      return closest;
    }

    double TotalAverageValue(ApolloRawData raw)
    {
      int sensors = 0;
      double total = 0;
      // Loop through all of the finger values (except the thumb)
      for (int bendPosition = 0; bendPosition < 8; bendPosition++)
      {
        sensors++;
        total += (raw == null) ? 0 : raw.flex(bendPosition);
      }

      return total / sensors;
    }
  }
}
