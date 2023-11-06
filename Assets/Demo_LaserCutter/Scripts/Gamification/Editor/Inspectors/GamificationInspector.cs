//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using UnityEditor;

namespace Gamification.EditorScripts.Inspectors
{
	[CustomEditor(typeof(GamificationController))] 
	public class GamificationInspector : Editor
	{
		void OnEnable()
		{
			UpdateInspector();
		}

		// OnInspector GUI
		public override void OnInspectorGUI() 
		{
			// Start a code block to check for GUI changes
			EditorGUI.BeginChangeCheck();

			base.OnInspectorGUI();

			// End the code block and update the label if a change occurred
			if (EditorGUI.EndChangeCheck())
			{
				UpdateInspector();
			}
		}
		private void UpdateInspector()
		{
			GamificationMenu.RefreshGamificationItems();
		}
	}
}

