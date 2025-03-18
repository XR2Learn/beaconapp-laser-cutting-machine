//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Gamification.Extention;
using Gamification.Evaluations;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.Help
{
	public class VisualGuidesCmd : Help, ICmd
	{
		private static List<XdeAsbStep> m_guidesOn;

		private List<XdeAsbVisualGuide> m_visualGuides;
		private List<XdeAsbStep> m_targetSteps;
		private IList<HelpConsumer> m_helpConsumers;

		public VisualGuidesCmd(GamificationController p_gamification, HelpController p_helpController) : base(p_gamification, p_helpController)
		{
			if (m_guidesOn == null)
			{
				m_guidesOn = new List<XdeAsbStep>();
			}
			
			m_visualGuides = p_helpController.VisualsGuides ?? throw new ArgumentNullException(nameof(p_helpController.VisualsGuides));
			List<XdeAsbStep> m_activeSteps = p_gamification.Scenarios.GetActiveSteps().Except(m_guidesOn).ToList();
			m_helpConsumers = m_evaluation.GetStepsEvaluators<HelpConsumer>(m_activeSteps);
			m_targetSteps = m_helpConsumers.Select(x => x.EvaluatedStep).ToList();
			
			//Needed to remove the visual guide when the step is completed
			foreach (XdeAsbStep l_step in m_activeSteps)
			{
				l_step.completedEvent.AddListener(StepCompleted); 
			}
		}

		public void Execute()
		{
			Debug.Log("[VisualGuidesCmd] Execute ");

			if (m_visualGuides.Count == 0)
			{
				Debug.Log("[VisualGuidesCmd] No visual guides available");
				return;
			}
			
			// Display visual guides on all activated steps (There maybe are some steps without AutonomyStep)
			DisplayVisualGuide(m_targetSteps);
			DisplayVisualGuide(m_guidesOn);
			m_guidesOn.AddRange(m_targetSteps);
			
			// Consume evaluation points
			foreach (HelpConsumer l_indep in m_helpConsumers)
			{
				l_indep.ConsumeHelp(this,m_helpScoringScale);
			}
		}

		public void DisplayVisualGuide(List<XdeAsbStep> p_steps)
		{
			foreach (XdeAsbStep l_step in p_steps)
			{
				m_visualGuides.Show(l_step);
			}
		}

		private void StepCompleted(XdeAsbStep p_step)
		{
			m_visualGuides.Hide(p_step);
			m_guidesOn.Remove(p_step);
			p_step.completedEvent.RemoveListener(StepCompleted);
		}
	}
}

