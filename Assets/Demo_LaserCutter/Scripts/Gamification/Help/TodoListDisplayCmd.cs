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
using Gamification.View;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.Help
{
	public class TodoListDisplayCmd : Help, ICmd
	{
		private TodoList m_todoList;
		private List<XdeAsbScenario> m_scenarios;
		public TodoListDisplayCmd(GamificationController p_gamification, HelpController p_helpController) : base(p_gamification,p_helpController)
		{
			m_todoList = p_helpController.TodoList ?? throw new ArgumentNullException(nameof(p_helpController.TodoList));
			m_scenarios = p_gamification.Scenarios ?? throw new ArgumentNullException(nameof(p_gamification.Scenarios));
		}
		
		public void Execute()
		{
			if (!m_todoList.IsActivated) 
			{
				Debug.Log("[TodoListDisplayCmd] Execute");
				DisplayActivateSteps();
			}
		}
		
		private void DisplayAllSteps()
		{
			List<HelpConsumer> l_helpConsumers = m_evaluation.GetStepsEvaluators<HelpConsumer>().ToList();
			l_helpConsumers = l_helpConsumers.FindAll(x => !x.EvaluatedStep.IsCompleted);
			
			List<XdeAsbStep> l_targetSteps = l_helpConsumers.Select(x => x.EvaluatedStep).ToList();
			
			m_todoList.StepList = GetStepsData(l_targetSteps);

			// Consume evaluation points
			foreach (HelpConsumer l_helpConsumer in l_helpConsumers)
			{
				l_helpConsumer.ConsumeHelp(this,m_helpScoringScale);
			}
		}

		private void DisplayActivateSteps()
		{
			List<XdeAsbStep> l_activeSteps = m_scenarios.GetActiveSteps();

			if (m_todoList.IsEqualToTodoList(l_activeSteps))
			{
				return;
			}
			IList<HelpConsumer> l_helpConsumers = m_evaluation.GetStepsEvaluators<HelpConsumer>(l_activeSteps);
			List<XdeAsbStep> l_targetSteps = l_helpConsumers.Select(x => x.EvaluatedStep).ToList();
			m_todoList.StepList = GetStepsData(l_targetSteps);
			// Consume evaluation points
			foreach (HelpConsumer l_helpConsumer in l_helpConsumers)
			{
				l_helpConsumer.ConsumeHelp(this,m_helpScoringScale);
			}
		}

		private Dictionary<XdeAsbStep, Chronometer> GetStepsData(List<XdeAsbStep> p_activeSteps = null)
		{
			Dictionary<XdeAsbStep, Chronometer> l_list = new Dictionary<XdeAsbStep, Chronometer>();

			List<XdeAsbStep> l_targetSteps = p_activeSteps ?? m_scenarios.GetSteps();
			IList<SafetyEvaluator> l_safety = m_evaluation.GetStepsEvaluators<SafetyEvaluator>(p_activeSteps);
			IList<Chronometer> l_chronos = m_evaluation.GetStepsEvaluators<Chronometer>(p_activeSteps);
			
			// Update and display TodoList
			foreach (SafetyEvaluator l_evaluationStep in l_safety)
			{
				// There maybe are some steps without chronometer
				l_list.Add(l_evaluationStep.EvaluatedStep,l_chronos.FirstOrDefault(x=>x.EvaluatedStep == l_evaluationStep.EvaluatedStep));
			}
			foreach (XdeAsbStep l_step in l_targetSteps.Except(l_safety.Select(x =>x.EvaluatedStep)))
			{
				// There maybe are some steps without chronometer
				l_list.Add(l_step,l_chronos.FirstOrDefault(x=>x.EvaluatedStep == l_step));
			}

			return l_list;
		}
	}
}

