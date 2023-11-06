//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using VMachina;

namespace Gamification.EditorScripts
{
	public static class HapticManagement
	{
		private static int m_sucessPaternIndex = 7;
		private static int m_dangerPaternIndex = 3;
		private static string m_hapticActionPath = "/actions/interact/out/ControllerVibration";
	
		public static void AddHapticManager(GamificationController p_gamification)
		{
			// Instanciate & add component
			GameObject l_hapticGO = new GameObject();
			l_hapticGO.name = "[HapticManager]";
			VRHapticManager l_haptic = l_hapticGO.AddComponent<VRHapticManager>();
			l_haptic.transform.SetParent(p_gamification.transform);
		
			// Set haptic manager value
			l_haptic.publicFrequency = 320;
			l_haptic.hapticAction = SteamVR_Action_Vibration.FindExistingActionForPartialPath(m_hapticActionPath) as SteamVR_Action_Vibration;
		
			// Set gamification controller events
			UnityAction<int> l_action = new UnityAction<int>(l_haptic.playPattern);
			UnityEventTools.AddIntPersistentListener(p_gamification.OnEachXdeAsbStepComplete, l_action, m_sucessPaternIndex);
			UnityEventTools.AddIntPersistentListener(p_gamification.OnDangerousCollision, l_action, m_dangerPaternIndex);

			// Select haptic manager
			Selection.activeTransform = l_hapticGO.transform;
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}
	}
}
