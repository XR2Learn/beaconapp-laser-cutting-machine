using UnityEditor;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;
using XdeEngine.Assembly;

namespace XdeEditor.Assembly
{
  public class XdeAsbManusVRHandGraspingInspector : Editor
  {
    SerializedProperty closeThresholdProp, echoTimeProp;
    SerializedProperty onRightHandOpenedProp, onRightHandClosedProp, onLeftHandOpenedProp, onLeftHandClosedProp;

    XdeAsbManusVRHandGrasping mhg;

    // Use this for initialization
    void OnEnable()
    {
      closeThresholdProp = serializedObject.FindProperty("closeThreshold");
      echoTimeProp = serializedObject.FindProperty("echoTime");

      onRightHandOpenedProp = serializedObject.FindProperty("onRightHandOpened");
      onRightHandClosedProp = serializedObject.FindProperty("onRightHandClosed");

      onLeftHandOpenedProp = serializedObject.FindProperty("onLeftHandOpened");
      onLeftHandClosedProp = serializedObject.FindProperty("onLeftHandClosed");
      
      mhg = (XdeAsbManusVRHandGrasping)target;
    }

    public override void OnInspectorGUI()
    {
      EditorGUILayout.IntSlider(closeThresholdProp, 0, 60, new GUIContent("Close grasping threshold"));
      EditorGUILayout.Slider(echoTimeProp, 0, 2, new GUIContent("Callback echo duration"));

      EditorGUILayout.PropertyField(onRightHandClosedProp, new GUIContent("On right hand closed"), true);
      EditorGUILayout.PropertyField(onRightHandOpenedProp, new GUIContent("On right hand opened"), true);

      EditorGUILayout.PropertyField(onLeftHandClosedProp, new GUIContent("On left hand closed"), true);
      EditorGUILayout.PropertyField(onLeftHandOpenedProp, new GUIContent("On left hand opened"), true);


      if (GUILayout.Button("Create Manus VR Hand Grasping Callbacks"))
      {
        DestroyHandGraspingCallbacks();
        CreateHandGraspingCallbacks();
      }

      serializedObject.ApplyModifiedProperties();
    }

    public void CreateHandGraspingCallbacks()
    {
      XdeAsbOperatorHands operatorHands = FindObjectOfType<XdeAsbOperatorHands>();
      if (operatorHands != null)
      {
        if (mhg.listenerIdAttachR == -1 && mhg.onRightHandClosed != null)
        {
          mhg.listenerIdAttachR = mhg.onRightHandClosed.GetPersistentEventCount();
          UnityAction attachRightAction = new UnityAction(operatorHands.AttachRightHand);
          UnityEventTools.AddVoidPersistentListener(mhg.onRightHandClosed, attachRightAction);
        }

        if (mhg.listenerIdDetachR == -1 && mhg.onRightHandOpened != null)
        {
          mhg.listenerIdDetachR = mhg.onRightHandOpened.GetPersistentEventCount();
          UnityAction detachRightAction = new UnityAction(operatorHands.DetachRightHand);
          UnityEventTools.AddVoidPersistentListener(mhg.onRightHandOpened, detachRightAction);
        }

        if (mhg.listenerIdAttachL == -1 && mhg.onLeftHandClosed != null)
        {
          mhg.listenerIdAttachL = mhg.onLeftHandClosed.GetPersistentEventCount();
          UnityAction attachLeftAction = new UnityAction(operatorHands.AttachLeftHand);
          UnityEventTools.AddVoidPersistentListener(mhg.onLeftHandClosed, attachLeftAction);
        }

        if (mhg.listenerIdDetachL == -1 && mhg.onLeftHandOpened != null)
        {
          mhg.listenerIdDetachL = mhg.onLeftHandOpened.GetPersistentEventCount();
          UnityAction detachLeftAction = new UnityAction(operatorHands.DetachLeftHand);
          UnityEventTools.AddVoidPersistentListener(mhg.onLeftHandOpened, detachLeftAction);
        }
      }
    }

    public void DestroyHandGraspingCallbacks()
    {
      if (mhg.listenerIdAttachR >= 0 && mhg.listenerIdAttachR < mhg.onRightHandClosed.GetPersistentEventCount())
        UnityEventTools.RemovePersistentListener(mhg.onRightHandClosed, mhg.listenerIdAttachR);

      mhg.listenerIdAttachR = -1;

      if (mhg.listenerIdDetachR >= 0 && mhg.listenerIdDetachR < mhg.onRightHandOpened.GetPersistentEventCount())
        UnityEventTools.RemovePersistentListener(mhg.onRightHandOpened, mhg.listenerIdDetachR);

      mhg.listenerIdDetachR = -1;

      if (mhg.listenerIdAttachL >= 0 && mhg.listenerIdAttachL < mhg.onLeftHandClosed.GetPersistentEventCount())
        UnityEventTools.RemovePersistentListener(mhg.onLeftHandClosed, mhg.listenerIdAttachL);

      mhg.listenerIdAttachL = -1;

      if (mhg.listenerIdDetachL >= 0 && mhg.listenerIdDetachL < mhg.onLeftHandOpened.GetPersistentEventCount())
        UnityEventTools.RemovePersistentListener(mhg.onLeftHandOpened, mhg.listenerIdDetachL);

      mhg.listenerIdDetachL = -1;
    }

    private void OnDestroy()
    {
      if (Application.isEditor)
      {
        if (target == null)
          DestroyHandGraspingCallbacks();
      }
    }
  }
}
