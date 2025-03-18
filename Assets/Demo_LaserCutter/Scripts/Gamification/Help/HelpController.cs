//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gamification.Evaluations;
using Gamification.Extention;
using Gamification.View;
using UnityEngine;
using XdeEngine.Assembly;
using XdeEngine.Core;

namespace Gamification.Help
{
	public class HelpController : MonoBehaviour
	{
		// Controllers
		[Header("Controllers")]
		[SerializeField]
		private HelpScoringScale m_helpScoringScale = null;
		[SerializeField]
		private TodoList m_todoList = null;
		[SerializeField]
		private bool m_showToDoAtStart = false;

		[Header("Buttons Press")] [SerializeField]
		private XdeRigidBody m_todoListBtn = null;

		[SerializeField] private XdeRigidBody m_nextBtn = null;
		[SerializeField] private XdeRigidBody m_visualGuidesBtn = null;

		private bool m_isNextStepRunning = false;
		private List<XdeAsbVisualGuide> m_visualGuides;

		// Actions
		private Action m_displayGuidlinesAction;
		private Func<Task> m_nextStepAction;
		private Action m_todoListAction;


		public HelpScoringScale HelpScoringScale => m_helpScoringScale;
		public TodoList TodoList => m_todoList;
		public List<XdeAsbVisualGuide> VisualsGuides => m_visualGuides;
		public XdeRigidBody TodoListBtn => m_todoListBtn;
		public XdeRigidBody NextBtn => m_nextBtn;
		public XdeRigidBody VisualGuidesBtn => m_visualGuidesBtn;

		public Action DisplayGuidlinesAction
		{
			set { m_displayGuidlinesAction = value; }
		}

		public Func<Task> NextStepAction
		{
			set { m_nextStepAction = value; }
		}

		public Action TodoListAction
		{
			set { m_todoListAction = value; }
		}

		private XdeAsbScenario m_mainScenario;

		public void Init(Evaluation p_evaluation, XdeAsbScenario p_mainScenario,
			List<XdeAsbScenario> p_optionalScenario)
		{
			if (m_todoList == null)
				throw new ArgumentNullException(nameof(m_todoList));
			if (m_helpScoringScale == null)
				throw new ArgumentNullException(nameof(m_helpScoringScale));

			// Add Chronometer to main scenario if it don't have one
			Chronometer l_chrono = p_mainScenario.GetComponentInChildrenFDS<Chronometer>();
			if (l_chrono == null)
			{
				l_chrono = AddEvaluationItem<Chronometer>(p_mainScenario.transform);
				l_chrono.EstimatedDuration = 5 * 60;
				l_chrono.PointValue = 0;
			}

			// Init items
			InitGuidelines(p_mainScenario, p_optionalScenario);
			m_todoList.Init(p_evaluation, p_mainScenario.name, l_chrono);

			m_mainScenario = p_mainScenario;
			m_mainScenario.completedEvent.AddListener(OnMainScenarioCompleted);
		}

		private void OnMainScenarioCompleted(XdeAsbStep p_arg0)
		{
			m_todoList.gameObject.SetActive(false);
		}
		private void InitGuidelines(XdeAsbScenario p_mainScenario, List<XdeAsbScenario> p_optionalScenario)
		{
			m_visualGuides = new List<XdeAsbVisualGuide>();
			List<XdeAsbScenario> l_scenarios = new List<XdeAsbScenario>();

			l_scenarios.Add(p_mainScenario);
			l_scenarios.AddRange(p_optionalScenario);

			// Collect visualguides on scenarios
			foreach (XdeAsbScenario l_scenario in l_scenarios)
			{
				m_visualGuides.AddRange(l_scenario.GetComponents<XdeAsbVisualGuide>());
			}

			DisableGuidlines();
		}

		public void EnableHelps(bool p_value)
		{
			m_todoList.gameObject.SetActive(p_value);
		}

		private void DisableGuidlines()
		{
			foreach (XdeAsbVisualGuide l_visualGuide in m_visualGuides)
			{
				l_visualGuide.enabled = false;
			}
		}

		//TODO Clean exist in GamificationMenu Editor => Make script Gamification utilities + static
		private I AddEvaluationItem<I>(Transform p_parent) where I : MonoBehaviour
		{
			I l_evaluationItem = p_parent.GetComponentInChildrenFDS<I>();

			if (l_evaluationItem == null)
			{
				GameObject l_itemGO = new GameObject();
				l_itemGO.name = "[" + typeof(I).Name + "]";
				l_itemGO.transform.parent = p_parent;
				l_evaluationItem = l_itemGO.AddComponent<I>();
			}

			return l_evaluationItem;
		}

		public void OnClickVisualGuides()
		{
			Debug.Log("Request visual guides");
			if (m_displayGuidlinesAction == null)
			{
				throw new NullReferenceException(nameof(m_displayGuidlinesAction));
			}

			m_displayGuidlinesAction.Invoke();
		}

		public void OnClickTodoList()
		{
			if (m_todoListAction == null)
			{
				throw new NullReferenceException(nameof(m_todoListAction));
			}
			Debug.Log("click todo list");
			m_todoListAction.Invoke();
		}

		[ContextMenu("ClickNext")]
		public void OnClickNext()
		{
			if (m_nextStepAction == null)
			{
				throw new NullReferenceException(nameof(m_nextStepAction));
			}

			NextStep();
		}

		public async Task NextStep()
		{
			if (!m_isNextStepRunning)
			{
				try
				{
					m_isNextStepRunning = true;
					await m_nextStepAction.Invoke();
				}
				finally
				{
					m_isNextStepRunning = false;
				}
			}
		}

		public void OnStartExercice()
		{
			if (m_showToDoAtStart)
				OnClickTodoList();
		}

		public void DisplayToDoListContent(bool p_value)
		{
			m_todoList.DisplayContent(p_value);
		}

		public void HideVisualGuidelines()
		{
			m_mainScenario.GetActiveSteps().ForEach(p_step => m_visualGuides.Hide(p_step));
			
		}
	}
}

