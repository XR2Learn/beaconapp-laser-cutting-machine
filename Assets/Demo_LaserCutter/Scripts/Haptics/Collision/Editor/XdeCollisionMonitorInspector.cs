using UnityEditor;
using UnityEngine;

namespace VMachina
{
  [CustomEditor(typeof(XdeCollisionMonitor))]
  public class XdeCollisionMonitorInspector : Editor
  {
    public override void OnInspectorGUI()
    {
      XdeCollisionMonitor monitor = (target as XdeCollisionMonitor);

      DrawDefaultInspector();

      serializedObject.Update();

      if (!Application.isPlaying)
      {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Contact Monitors"))
          monitor.CreateContactMonitors();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Interference Monitors"))
          monitor.CreateInterferenceMonitors();
        EditorGUILayout.EndHorizontal();
      }

      serializedObject.ApplyModifiedProperties();
    }
  }
}