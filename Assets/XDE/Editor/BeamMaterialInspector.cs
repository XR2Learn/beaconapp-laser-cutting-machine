using UnityEditor;
using UnityEngine;
using xde.unity.math;
using XdeEngine.Core;
using System;

namespace XdeEditor.Core
{
  [CustomEditor(typeof(BeamMaterial))]
  public class BeamMaterialInspector : Editor
  {
    public override void OnInspectorGUI()
    {
      BeamMaterial bm = target as BeamMaterial;
      EditorGUILayout.PropertyField(this.serializedObject.FindProperty("mat_name"), new GUIContent("Name"));
      EditorGUILayout.LabelField("Physical properties", EditorStyles.boldLabel);


      EditorGUILayout.PropertyField(this.serializedObject.FindProperty("damping"), new GUIContent("Damping ratio"));
      EditorGUILayout.PropertyField(this.serializedObject.FindProperty("stiffnessComputationMode"), new GUIContent("Parametrization Mode"));
      if (bm.stiffnessComputationMode == xde_types.core.beam_stiffness_computation_mode.material_layers)
      {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Linear mass " + String.Format("{0:0.000 kg/m}",bm.LinearMass));
        bool compo_error = (bm.composition == null || bm.composition.Length == 0);
        if (compo_error)
          GUILayout.Label("need at least one material in composition", Styles.FailedLabelStyle);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("composition"), new GUIContent("Layer composition"), true);
        if (EditorGUI.EndChangeCheck())
        {
          serializedObject.ApplyModifiedProperties();
          bm.ComputeStiffnessValues();
          /*Unity works in mysterious ways.
           * After a long journey it has come evident to me that I should expect the unexpected
           * Sometimes when calling a method on a target object, the Editor sentience will not consider the impact it could have on serialized element
           * Hence an Update will work as a brief prayer, calling attention from the etherial power to our humble changed
           * But only after a previous rite of ApplyModifiedProperties has cemented the values we have already changed
           * Be this knowledge shared, for it has costed us greatly to identify in the past
           * */
          this.serializedObject.Update();
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stiffness values preview");
        GUI.enabled = false;
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("previewStiffness.torsionalStiffness"), new GUIContent("Torsional Stiffness (N*m^2)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("previewStiffness.bendingStiffnessY"), new GUIContent("Bending Stiffness Y (N*m^2)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("previewStiffness.bendingStiffnessZ"), new GUIContent("Bending Stiffness Z (N*m^2)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("previewStiffness.axialStiffness"), new GUIContent("Axial Stiffness (N)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("previewStiffness.transverseShearStiffnessY"), new GUIContent("Transverse Shear Stiffness Y (N)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("previewStiffness.transverseShearStiffnessZ"), new GUIContent("Transverse Shear Stiffness Z (N)"), true);
        GUI.enabled = true;
      }
      else
      {
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("linearMass"), new GUIContent("Linear Mass (Kg/m)"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("radius"), new GUIContent("Radius (m)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("diagonalStiffness.torsionalStiffness"), new GUIContent("Torsional Stiffness (N*m^2)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("diagonalStiffness.bendingStiffnessY"), new GUIContent("Bending Stiffness Y (N*m^2)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("diagonalStiffness.bendingStiffnessZ"), new GUIContent("Bending Stiffness Z (N*m^2)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("diagonalStiffness.axialStiffness"), new GUIContent("Axial Stiffness (N)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("diagonalStiffness.transverseShearStiffnessY"), new GUIContent("Transverse Shear Stiffness Y (N)"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("diagonalStiffness.transverseShearStiffnessZ"), new GUIContent("Transverse Shear Stiffness Z (N)"), true);
      }

      serializedObject.ApplyModifiedProperties();
    }
  }
}


