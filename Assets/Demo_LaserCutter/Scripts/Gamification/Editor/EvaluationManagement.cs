//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections.Generic;
using Gamification.Evaluations;
using Gamification.Evaluations.Metrics;
using Gamification.Extention;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.EditorScripts
{
	public static class EvaluationManagement
	{
		public static void AddEvaluationStep<I>() where I:MonoBehaviour
		{
			if (Selection.activeTransform != null)
			{
				GameObject l_itemGO = null;
				foreach (Transform l_transform in Selection.transforms)
				{
					XdeAsbStep l_step = l_transform.GetComponent<XdeAsbStep>();
					if (l_step != null)
					{
						l_itemGO = AddEvaluationStep<I>(l_step).gameObject;
					}
					else
					{
						Debug.Log("[GamificationMenu] - AddEvaluationStep : Select a step (XdeAsbStep) ->"+l_transform.name);
					}
				}

				if (Selection.objects.Length == 1 && l_itemGO!=null)
				{
					Selection.activeTransform = l_itemGO.transform;
				}
			}
			else
			{
				Debug.Log("[GamificationMenu] - AddEvaluationStep : Select a step to evaluate");
			}
		}
		
		public static I AddEvaluationStep<I>(XdeAsbStep p_parent) where I : MonoBehaviour
		{
			I l_evaluationItem = p_parent.GetComponentInChildrenFDS<I>();
			if (l_evaluationItem == null)
			{
				GameObject l_itemGO = new GameObject();
				l_itemGO.name = "[" + typeof(I).Name + "]";
				l_itemGO.transform.parent = p_parent.transform;
				l_evaluationItem = l_itemGO.AddComponent<I>();
			}
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			return l_evaluationItem;
		}

		public static void AddEvaluation(GamificationController p_gamification)
		{
			GameObject l_evaluationGO = new GameObject();
			l_evaluationGO.name = "[Evaluation]";
			Evaluation l_evaluation = l_evaluationGO.AddComponent<Evaluation>();
			
			l_evaluation.transform.SetParent(p_gamification.transform);

			p_gamification.Evaluation = l_evaluation;
			Selection.activeTransform = l_evaluationGO.transform;
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}
		
		
		public static void UpdateEvaluationMetrics(Evaluation p_evaluation, List<XdeAsbScenario> p_scenarios)
		{
			if (p_evaluation == null && p_scenarios!= null)
				return;
			
			UpdateMetrics<EfficiencyMetric>(p_evaluation,p_scenarios);
			UpdateMetrics<SafetyMetric>(p_evaluation,p_scenarios);
			UpdateMetrics<AutonomyMetric>(p_evaluation,p_scenarios);
		}
		
		private static void UpdateMetrics<M>(Evaluations.Evaluation p_evaluation, List<XdeAsbScenario> p_scenarios) where M:Metric
		{
			if (p_scenarios == null)
				return;

			List<IStepEvaluator> l_evaluationSteps = new List<IStepEvaluator>();
			foreach (XdeAsbScenario l_parent in p_scenarios)
			{
				l_evaluationSteps.AddRange(l_parent.GetComponentsInChildren<IStepEvaluator>(true));
			}
			l_evaluationSteps = l_evaluationSteps.FindAll(x=>x.MetricType == typeof(M));
			
			M l_metric = p_evaluation.GetComponentInChildren<M>();
			if (l_evaluationSteps.Count > 0 && l_metric == null)
			{
				AddMetric<M>(p_evaluation);
			}
			else if (l_evaluationSteps.Count == 0 && l_metric != null)
			{
				Object.DestroyImmediate(l_metric.gameObject);
			}
		}
		
		private static void AddMetric<M>(Evaluation p_evaluation) where M:Metric 
		{
			GameObject l_itemGO = new GameObject();
			l_itemGO.name = "["+typeof(M).Name+"]";
			l_itemGO.transform.parent = p_evaluation.transform;
			M l_metric = l_itemGO.AddComponent<M>();
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			Debug.LogFormat("[EvaluationMenu] - Add "+typeof(M).ToString()+" metric to evaluation : "+p_evaluation.name);
		}
	}
}
