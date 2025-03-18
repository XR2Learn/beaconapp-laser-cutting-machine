using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR;
using xde.unity;
using XdeEngine.Core;
using Zenject;

public class LayerMigrationTool : Editor
{
		[MenuItem("INTERACT/Migrate Layers to new PM")]
		static void Migrate()
		{
			
				GameObject l_oldPhysicsManager = GameObject.Find("[PhysicsManager]");
				l_oldPhysicsManager.name = "[PhysicsManager]_old";
				GameObject l_newPhysicsManager = CreatePhysicsManager();
				XdeLayers l_oldLayers = l_oldPhysicsManager.GetComponent<XdeLayers>();
				XdeLayers l_newLayers = l_newPhysicsManager.GetComponent<XdeLayers>();
				l_newLayers.layers = l_oldLayers.layers;
				l_newLayers.matrix = l_oldLayers.matrix;
				l_newLayers.logImpacts = l_oldLayers.logImpacts;
				l_newLayers.impactDectectionThreshold = l_oldLayers.impactDectectionThreshold;
				l_newPhysicsManager.AddComponent<XdeContactLaws>().laws = l_oldPhysicsManager.GetComponent<XdeContactLaws>().laws;
				DestroyImmediate(l_oldPhysicsManager);
				XdeRigidBody[] l_rigidBodies = FindObjectsOfType<XdeRigidBody>();
				foreach (XdeRigidBody l_rigidB in l_rigidBodies)
				{
					XdeLayersTag l_layer = l_rigidB.layers;
					List<XdeLayerRef> l_refs = new List<XdeLayerRef>();
					XdeLayerRef l_nRef = new XdeLayerRef {layer = l_layer.refs[0].layer, matrix = l_newLayers};
					l_refs.Add(l_nRef);
					l_layer.refs = l_refs;
				}
		}

		private static GameObject CreatePhysicsManager()
		{
			GameObject l_root = new GameObject("[PhysicsManager]");
			l_root.AddComponent<SceneContext>();
			l_root.AddComponent<XdeEngine.Core.Installer>();
			l_root.GetComponent<SceneContext>().Installers = new MonoInstaller[]
				{l_root.GetComponent<XdeEngine.Core.Installer>()};
			XdeScene l_RootScene = l_root.AddComponent<XdeScene>();
			l_RootScene.autoConnect = false;
			l_RootScene.contactMonitoring = true;
			l_RootScene.timeStep = 0.01;
			l_RootScene.LMDmax = 0.02;
			l_RootScene.directSolverType = xde_types.core.gvm_direct_solver.Skyline;
			l_root.AddComponent<XdeLayers>();
			l_root.AddComponent<StartServers>();
			l_root.AddComponent<XdeEngine.Core.Monitoring.XdeArrowDisplay>();
			//l_root.GetComponent<XdeEngine.Core.Monitoring.XdeArrowDisplay>().enabled =
				//InteractPreferences.ShowContactArrows;
			l_root.GetComponent<XdeEngine.Core.Monitoring.XdeArrowDisplay>().scale = 0.05f;
			l_root.GetComponent<XdeEngine.Core.Monitoring.XdeArrowDisplay>().size =
				XdeEngine.Core.Monitoring.XdeArrowDisplay.ArrowSizingMethod.ForceBased;
			l_root.AddComponent<XdeEngine.Core.Monitoring.XdeInterferenceDisplay>();
			l_root.GetComponent<XdeEngine.Core.Monitoring.XdeInterferenceDisplay>().kColor = Color.yellow;
			l_root.AddComponent<InteractSelectionManager>().m_outlineMaterial =
				(Material) Resources.Load("Selector/Materials/SelectionMat", typeof(Material));
			l_root.AddComponent<NoteManager>();
			l_root.AddComponent<OctopclRaycastScene>();
			l_root.GetComponent<OctopclRaycastScene>().resolution = 0.01f;
			l_root.AddComponent<Painter>().m_myModeManager = l_root.AddComponent<MyModeManager>();
			l_root.AddComponent<XdeNetwork.XdeRPCApi>();
			l_root.AddComponent<SteamVR_ActivateActionSetOnLoad>();
			l_root.GetComponent<SteamVR_ActivateActionSetOnLoad>().actionSet = SteamVR_Input.GetActionSet("interact");
			
			return l_root;
		}
}
