//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections.Generic;
using System.Linq;
using Gamification.Evaluations;
using Gamification.Help;
using UnityEditor;

namespace Gamification.EditorScripts.Inspectors
{
	[CustomEditor(typeof(HelpScoringScale))]
public class HelpScoringScaleInspector : Editor
{
	private float m_nbHelpConsumer;
	private HelpScoringScale m_helpScoringScale;
	HelpScoringScaleInspector()
	{
		EditorApplication.hierarchyChanged += RefreshValues;
	}

	void OnEnable()
	{
		m_helpScoringScale = (HelpScoringScale)target;
		RefreshValues();
	}

	public override void OnInspectorGUI() 
	{
		EditorGUI.BeginChangeCheck();
		
		base.OnInspectorGUI();

		// End the code block and update the label if a change occurred
		if (EditorGUI.EndChangeCheck())
		{
			RefreshValues();
		}
		
		EditorGUILayout.Space(20);
		EditorGUILayout.LabelField("TodoList");
		EditorGUILayout.LabelField("Usage: The task list can be displayed only once during the exercise. Then it stays displayed.");
		EditorGUILayout.LabelField("Display TodoList costs : the sum (for each help consumer of uncompleted step) of the product (point value) x (cost of the todolist) ");
		EditorGUILayout.LabelField("( Evaluation contains "+m_nbHelpConsumer+" AutonomySteps)");
		EditorGUILayout.Space(20);
		EditorGUILayout.LabelField("VisualGuides");
		EditorGUILayout.LabelField("Use: Visual guides of activated steps can be displayed at any time during the exercise.");
		EditorGUILayout.LabelField("Display visual guides cost (points): "+ m_helpScoringScale.VisualGuidesCoef.ToString()+" x AutonomySteps PointValue");
		EditorGUILayout.Space(20);
		EditorGUILayout.LabelField("Next Step");
		EditorGUILayout.LabelField("Use: the next step will bring a zero score on all its available metrics.");
	}
	
	public void RefreshValues()
	{
		m_nbHelpConsumer = 0;
		List<HelpConsumer> l_helpConsumers;
		GamificationController l_controller = GamificationMenu.GetGamificationController();
		if (l_controller != null && l_controller.Evaluation != null)
		{
			l_helpConsumers = l_controller.Evaluation.GetAllStepsEvaluators(l_controller.Scenarios).OfType<HelpConsumer>().ToList();
			m_nbHelpConsumer = l_helpConsumers.Count;
		}
	}
}
}

