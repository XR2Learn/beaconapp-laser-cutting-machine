//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.IO;
using Gamification.Evaluations;
using Gamification.View;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.EditorScripts
{
	public static class GamificationMenu 
	{
		private static string m_gamificationName = "[GamificationManager]";
		private static string m_gamificationPrefPath = "Assets/VME/Gamification/Prefabs/3D/";
		private static string m_startupPrefPath = Path.Combine(m_gamificationPrefPath,"[StartupScreen].prefab");
		private static string m_endingPrefPath = Path.Combine(m_gamificationPrefPath,"[EndingScreen].prefab");
		private static string m_helpOperatorPrefPath = Path.Combine(m_gamificationPrefPath,"[HelpRequestPost].prefab");
		
		static GamificationMenu()
		{
			EditorApplication.hierarchyChanged += RefreshGamificationItems;
		}
		
		public static void RefreshGamificationItems()
		{
			GamificationController l_controller = GetGamificationController();
			if (l_controller != null)
			{
				EvaluationManagement.UpdateEvaluationMetrics(l_controller.Evaluation,l_controller.Scenarios);
			}
		}
		
		public static GamificationController GetGamificationController()
		{
			GamificationController l_controller;
			GameObject l_gamificationGO = GameObject.Find(m_gamificationName);

			if (l_gamificationGO != null)
			{
				l_controller = l_gamificationGO.GetComponent<GamificationController>();
				if (l_controller == null)
				{
					l_controller = l_gamificationGO.AddComponent<GamificationController>();
				}
				return l_controller;
			}
			return null;
		}

		private static GamificationController AddGamificationController(XdeAsbScenario p_scenario)
		{
			// Add Gameobject and Component
			GameObject l_itemGO = new GameObject(m_gamificationName);
			GamificationController l_controller = l_itemGO.AddComponent<GamificationController>();
			
			// Set main scenario attribute
			l_controller.MainScenario = p_scenario;
			
			// Add chronometer ti main scenario
			Chronometer l_chronometer = EvaluationManagement.AddEvaluationStep<Chronometer>(l_controller.MainScenario);
			l_chronometer.PointValue = 0;
			l_chronometer.EstimatedDuration = 5 * 60;

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			
			return l_controller;
		}

		[MenuItem("GameObject/GAMIFICATION/+ Gamification Pack",false, 0)]
		public static void AddGamificationPack()
		{
			if (Selection.activeTransform != null)
			{
				XdeAsbScenario l_scenario = Selection.activeTransform.GetComponent<XdeAsbScenario>();
				if (l_scenario != null)
				{
					GamificationController l_controller = AddGamificationController(l_scenario);
					EvaluationManagement.AddEvaluation(l_controller);
					HelpManagement.AddHelpRequestPost(l_controller,m_helpOperatorPrefPath);
					AddGamificationScreens(l_controller);
					Selection.activeTransform = l_controller.transform;
				}
				else
				{
					Debug.Log("[GamificationMenu] - AddGamificationPack : Select a scenario (XdeAsbScenario) ->"+Selection.activeTransform.name);
				}
			}
			else
			{
				Debug.Log("[GamificationMenu] - AddGamificationPack : Select a scenario to evaluate");
			}
		}
		
		
		[MenuItem("GameObject/GAMIFICATION/Step evaluators/+ Chronometer	[Efficiency]",false, 10)]
		public static void AddEfficiencyItem()
		{
			EvaluationManagement.AddEvaluationStep<Chronometer>();
		}
		
		[MenuItem("GameObject/GAMIFICATION/Step evaluators/+ SafetyEvaluator	[Safety]",false, 10)]
		public static void AddSafetyItem()
		{
			EvaluationManagement.AddEvaluationStep<SafetyEvaluator>();
		}
		
		[MenuItem("GameObject/GAMIFICATION/Step evaluators/+ CollisionTracker	[Safety]",false, 10)]
		public static void AddCollisionItem()
		{
			EvaluationManagement.AddEvaluationStep<CollisionTracker>();
		}
		
		[MenuItem("GameObject/GAMIFICATION/Step evaluators/+ HelpConsumer	[Autonomy]",false, 10)]
		public static void AddIndependencyItem()
		{
			EvaluationManagement.AddEvaluationStep<HelpConsumer>();
		}
		
		[MenuItem("GameObject/GAMIFICATION/Gamification components/+ Evaluation",false, 10)]
		public static void AddEvaluation()
		{
			GamificationController l_controller = GetGamificationController();
			if (l_controller != null)
			{
				EvaluationManagement.AddEvaluation(l_controller);
			}
			else
			{
				Debug.Log("[GamificationMenu] - Instanciate Gamification controller first");
			}
		}
		
		[MenuItem("GameObject/GAMIFICATION/Gamification components/+ HelpRequestPost",false, 10)]
		public static void AddHelpRequestPost()
		{ 
			GamificationController l_controller = GetGamificationController();
			if (l_controller != null)
			{
				HelpManagement.AddHelpRequestPost(l_controller,m_helpOperatorPrefPath);
			}
			else
			{
				Debug.Log("[GamificationMenu] - Instanciate Gamification controller first");
			}
		}

		[MenuItem("GameObject/GAMIFICATION/Gamification components/ + Screens",false, 10)]
		public static void AddGamificationScreens()
		{
			GamificationController l_controller = GetGamificationController();
			if (l_controller != null)
			{
				AddGamificationScreens( l_controller);
			}
			else
			{
				Debug.Log("[GamificationMenu] - Instanciate Gamification controller first");
			}
		}
		
		
		[MenuItem("GameObject/GAMIFICATION/Gamification components/ + Gamification Controller",false, 10)]
		public static void AddGamificationController()
		{
			if (Selection.activeTransform != null)
			{
				XdeAsbScenario l_scenario = Selection.activeTransform.GetComponent<XdeAsbScenario>();
				if (l_scenario != null)
				{
					AddGamificationController(l_scenario);
				}
				else
				{
					Debug.Log("[GamificationMenu] - AddGamificationPack : Select a scenario (XdeAsbScenario)");
				}
			}
			else
			{
				Debug.Log("[GamificationMenu] - AddGamificationPack : Select a scenario to evaluate");
			}
		}
		
		[MenuItem("GameObject/HAPTIC/ + Haptic manager",false, 1)]
		public static void AddHapticManager()
		{
			GamificationController l_controller = GetGamificationController();
			if (l_controller != null)
			{
				HapticManagement.AddHapticManager(l_controller);
			}
			else
			{
				Debug.Log("[GamificationMenu] - Instanciate Gamification controller first");
			}
		}
		
		private static void AddGamificationScreens(GamificationController p_controller)
		{
			StartupScreen l_startScreenPref = (StartupScreen)AssetDatabase.LoadAssetAtPath(m_startupPrefPath, typeof(StartupScreen));
			EndingScreen l_endScreenPref = (EndingScreen)AssetDatabase.LoadAssetAtPath(m_endingPrefPath, typeof(EndingScreen));
			StartupScreen l_startScreen = (StartupScreen)PrefabUtility.InstantiatePrefab(l_startScreenPref);
			EndingScreen l_endScreen = (EndingScreen)PrefabUtility.InstantiatePrefab(l_endScreenPref);
			PrefabUtility.UnpackPrefabInstance(l_startScreen.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
			PrefabUtility.UnpackPrefabInstance(l_endScreen.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
			
			l_startScreen.transform.SetParent(p_controller.transform);
			l_endScreen.transform.SetParent(p_controller.transform);

			p_controller.StartupScreen = l_startScreen;
			p_controller.EndingScreen = l_endScreen;
			Selection.activeTransform = l_startScreen.transform;
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}
	}
}


