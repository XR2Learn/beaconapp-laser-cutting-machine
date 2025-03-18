//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
 using System.Collections.Generic;
using System.Linq;
 using Gamification.Evaluations.Metrics;
 using UnityEngine;
using XdeEngine.Assembly;
 
 namespace Gamification.Evaluations
 {
	 public class SafetyEvaluator : StepEvaluator<SafetyMetric>
	 {
		[SerializeField]
		private List<XdeAsbStep> m_targetSteps = null;
		private List<XdeAsbStep> m_validatedTargetSteps;
		
		protected override void OnEnable()
		{
			base.OnEnable();
			m_validatedTargetSteps = new List<XdeAsbStep>();
		}
	 
		private void InitTargetSteps()
		{
			if(m_targetSteps == null)
				 throw new Exception("No XdeAsStep as target step for SafetyEvaluation");
	 
			foreach (XdeAsbStep l_step in m_targetSteps)
			{
				l_step.completedEvent.AddListener(OnTargetStepComplete); 
			}
		}
		
		public void OnTargetStepComplete(XdeAsbStep p_step)
		{
			if (EvaluatedStep.IsCompleted)
			{
				Debug.Log("[SafetyStep] - OnTargetStepComplete : add "+p_step.name+" to ValidatedTargetSteps list.");
				m_validatedTargetSteps.Add(p_step);
			}
			p_step.completedEvent.RemoveListener(OnTargetStepComplete); 
		}
	 
		public override float GetScore()
		{
			if (EvaluatedStep.IsCompleted && m_targetSteps.Count == m_validatedTargetSteps.Count)
			{
				return m_pointValue;
			}
	 
			return 0;
		}
	 
		public override int BonusMalusLogic
		{
			get
			{
				return (int)BonusMalus.Bonus;
			}
		}
	 
		public override string GetLogs(bool p_displayPoints = false)
		{
			List<XdeAsbStep> l_missedSteps = m_targetSteps.Except(m_validatedTargetSteps).ToList(); 
			string l_state = string.Empty;
			if ((m_targetSteps.Count == 0 && EvaluatedStep.IsCompleted) || (m_targetSteps.Count != 0 && l_missedSteps.Count == 0))
			{
				l_state = "task completed.";
			}
			else if (l_missedSteps.Count>0)
			{
				l_state = "task uncompleted at ";
				foreach (XdeAsbStep l_step in l_missedSteps)
				{
					l_state += l_step.name + "/";
				}
			}
			else
			{
				l_state = "task uncompleted.";
			}
			return ToString("Safety",p_displayPoints,l_state);
		}
	 
		protected override void OnActivate(XdeAsbStep p_step)
		{
			base.OnActivate(p_step);
			InitTargetSteps();
		}
	 }
 }

