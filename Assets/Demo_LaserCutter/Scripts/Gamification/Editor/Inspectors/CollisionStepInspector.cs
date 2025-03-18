//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Linq;
using Gamification.Evaluations;
using UnityEditor;

namespace Gamification.EditorScripts.Inspectors
{
	[CustomEditor(typeof(CollisionTracker))] 
	public class CollisionStepInspector : Editor
	{
		private CollisionTracker m_collisionTracker;
		void OnEnable()
		{
			m_collisionTracker = (CollisionTracker) target;
			UpdateInspector();
		}

		// OnInspector GUI
		public override void OnInspectorGUI() 
		{
			// Start a code block to check for GUI changes
			EditorGUI.BeginChangeCheck();

			base.OnInspectorGUI();
		
			EditorGUILayout.Space(20);
			EditorGUILayout.LabelField("* The number of points is calculated automatically here.");
			EditorGUILayout.LabelField("* It corresponds to the sum of the object collision penality.");
			EditorGUILayout.Space(20);

			// End the code block and update the label if a change occurred
			if (EditorGUI.EndChangeCheck())
			{
				UpdateInspector();
			}
		}
		private void UpdateInspector()
		{
			if (m_collisionTracker.DangerousObjects == null || m_collisionTracker.DangerousObjects.Count == 0)
			{
				m_collisionTracker.PointValue = 0;
			}
			else
			{
				m_collisionTracker.PointValue = m_collisionTracker.DangerousObjects.Sum(x => x.Penality);
			}
		}
	}
}

