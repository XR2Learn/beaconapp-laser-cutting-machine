//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Gamification.Evaluations;
using Gamification.Evaluations.Metrics;
using UnityEngine;
using UnityEngine.UI;
using XdeEngine.Assembly;

namespace Gamification.View
{
	public class TodoList : MonoBehaviour
	{
		[Header("Reference")]
		[SerializeField]
		private Text m_scenarioName = null;
		[SerializeField]
		private Text m_scenarioChrono = null;
		[SerializeField]
		private Transform m_content = null;
		
		[Header("Prefabs")]
		[SerializeField]
		private Todo m_todoPref = null;
		
		[Header("Sprites")]
		[SerializeField]
		private Texture2D m_bonusAutonomy = null;
		[SerializeField]
		private Texture2D m_malusAutonomy = null;
		[SerializeField]
		private Texture2D m_bonusEfficiency = null;
		[SerializeField]
		private Texture2D m_malusEfficiency = null;
		[SerializeField]
		private Texture2D m_bonusSafety = null;
		[SerializeField]
		private Texture2D m_malusSafety = null;
		
		private bool m_isSynchro = false;
		private Chronometer m_chrono;
		private List<Todo> m_todolist;
		private Evaluation m_evaluation;
		public bool IsActivated { get; private set; }
		public bool IsInitialized { get; private set; }
		
		public Chronometer Chrono 
		{
			set
			{
				if (value != null)
				{
					m_chrono = value;
					TimeSpan l_timeSpan = TimeSpan.FromSeconds(m_chrono.TimeElapsed);
					m_scenarioChrono.text = l_timeSpan.ToString(@"mm\:ss");
					m_isSynchro = true;
				}
			}
		}

		public Dictionary<XdeAsbStep,Chronometer> StepList
		{
			set
			{
				if (value!= null)
				{
					Clear();
					m_todolist = new List<Todo>();
					IsInitialized = true;

					foreach (KeyValuePair<XdeAsbStep,Chronometer> l_pair in value)
					{
						Todo l_todo = AddTask(l_pair.Key,l_pair.Value);
						m_todolist.Add(l_todo);
						l_pair.Key.completedEvent.AddListener(l_todo.OnStepComplete);
						l_pair.Key.completedEvent.AddListener(this.OnStepComplete);
					}

					DisplayContent(true);
				}
			}
		}
		
		
		public void Init(Evaluation p_evaluation, string p_scenarioName, Chronometer p_scenarioChronometer)
		{
			m_evaluation = p_evaluation;
			IsInitialized = false;
			DisplayContent(false);
			m_scenarioName.text = p_scenarioName;
			Chrono = p_scenarioChronometer;
			this.gameObject.SetActive(false);
		}

		private Todo AddTask(XdeAsbStep p_step, Chronometer p_chronometer)
		{
			Todo l_todo = Instantiate(m_todoPref, m_content);
			l_todo.Name = p_step.name;
			l_todo.IsDone = p_step.IsCompleted;
			if (l_todo.IsDone)
				l_todo.GetComponent<Image>().color = new Color(33f / 255f, 33f / 255f, 33f / 255f);
			l_todo.transform.SetParent(m_content);
			l_todo.Chrono = p_chronometer;
			
			l_todo.GetStepPictoResult = GetStepPictoResults;
			
			return l_todo;
		}

		private void Clear()
		{
			foreach (Transform child in m_content) 
			{
				GameObject.Destroy(child.gameObject);
			}
		}

		private void OnStepComplete(XdeAsbStep p_step)
		{
			p_step.completedEvent.RemoveListener(OnStepComplete);
			DisplayContent(false);
		}

		private List<Texture2D> GetStepPictoResults(XdeAsbStep p_step)
		{
			List<Texture2D> l_pictoResults = new List<Texture2D>();
			foreach (KeyValuePair<Type,bool> l_pair in m_evaluation.GetStepResults(p_step))
			{
				l_pictoResults.Add(GetTodoBonusMalus(l_pair.Key,l_pair.Value));
			}
			return l_pictoResults;
		}

		public Texture2D GetTodoBonusMalus(Type p_metric, bool p_isBonus)
		{
			Texture2D l_texture;
			if (p_metric == typeof(AutonomyMetric))
				l_texture = (p_isBonus ? m_bonusAutonomy : m_malusAutonomy);
			else if (p_metric == typeof(EfficiencyMetric))
				l_texture = (p_isBonus ? m_bonusEfficiency : m_malusEfficiency);
			else if (p_metric == typeof(SafetyMetric))
				l_texture = (p_isBonus ? m_bonusSafety : m_malusSafety);
			else
				l_texture = null;

			return l_texture;
		}

		private void Update()
		{
			if (m_isSynchro)
			{
				TimeSpan l_timeSpan = TimeSpan.FromSeconds(m_chrono.TimeElapsed);
				m_scenarioChrono.text = l_timeSpan.ToString(@"mm\:ss");
			}
		}

		public void DisplayContent(bool p_value)
		{
			Debug.Log("Display Todo List " + p_value);
			m_content.gameObject.SetActive(p_value);
			IsActivated = p_value;
		}

		public bool IsEqualToTodoList(List<XdeAsbStep> p_xdeAsbSteps)
		{
			if(m_todolist == null)
				return false;

			List<string> l_todoNames = m_todolist.Select(p_todo => p_todo.Name).ToList();
			List<string> l_stepsNames = p_xdeAsbSteps.Select(p_xdeAsbStep => p_xdeAsbStep.name).ToList();

			return l_todoNames.SequenceEqual(l_stepsNames);
		}
	}
}


