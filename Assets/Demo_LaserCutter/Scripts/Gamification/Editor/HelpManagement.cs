//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections.Generic;
using System.Linq;
using Gamification.Help;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using XdeEngine.Core;

namespace Gamification.EditorScripts
{
	public static class HelpManagement
	{
		private static int m_helpButtonLayerIndex = 11;
		private static string m_helpButtonLayerName = "00_Others";

		public static void AddHelpRequestPost(GamificationController p_gamification, string p_helpOperatorPrefPath)
		{
			HelpController l_helpRequestPostPref = (HelpController)AssetDatabase.LoadAssetAtPath(p_helpOperatorPrefPath, typeof(HelpController));
			HelpController l_helpController =  (HelpController)PrefabUtility.InstantiatePrefab(l_helpRequestPostPref);
			PrefabUtility.UnpackPrefabInstance(l_helpController.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
			Selection.activeObject = l_helpController;
			Selection.activeTransform.SetParent(p_gamification.transform);
			p_gamification.HelpController = l_helpController;
			
			// Physicalize help buttons
			PhysicalizeXdeRigidBody(l_helpController, l_helpController.TodoListBtn);
			PhysicalizeXdeRigidBody(l_helpController, l_helpController.NextBtn);
			PhysicalizeXdeRigidBody(l_helpController, l_helpController.VisualGuidesBtn);
			
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		private static void PhysicalizeXdeRigidBody(HelpController p_helpRequestPost, XdeRigidBody p_rigidBody)
		{
			XdeLayers l_physicsManager = GetXdeLayers();
			if (l_physicsManager != null)
			{
				XdeLayerRef l_layerRef = new XdeLayerRef();
				// Set a layer that can collide body P1
				l_layerRef.matrix = l_physicsManager;
				int l_layerIndex = l_physicsManager.FindLayer(m_helpButtonLayerName);
				if (l_layerIndex > l_physicsManager.layers.Count || l_layerIndex < 0)
				{
					l_layerIndex = 0;
				}
				l_layerRef.layer = l_layerIndex;
				p_rigidBody.layers.refs.Add(l_layerRef);
			}
			else
			{
				Debug.Log("[HelpManagement] There is no PhysicsManager or XdeLayers in scène. Create one and delete and re-import HelpRequestPost (so collision layers could be added to the buttons).");
			}
		}

		private static XdeLayers GetXdeLayers()
		{
			List<XdeLayers> l_xdeLayersList = GetAllObjectsOnlyInScene<XdeLayers>();
			XdeLayers l_xdeLayers = l_xdeLayersList.Count == 0 ? null : l_xdeLayersList.First();
			return l_xdeLayers;
		}
		
		private static List<T> GetAllObjectsOnlyInScene<T>() where T:Component
		{
			List<T> objectsInScene = new List<T>();

			foreach (T go in Resources.FindObjectsOfTypeAll(typeof(T)) as T[])
			{
				if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
					objectsInScene.Add(go);
			}

			return objectsInScene;
		}
	}
}
