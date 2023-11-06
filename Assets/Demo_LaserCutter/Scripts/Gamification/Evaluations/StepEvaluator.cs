//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using Gamification.Evaluations.Metrics;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.Evaluations
{
	public abstract class StepEvaluator<T> : MonoBehaviour, IStepEvaluator where T:Metric
	{
		protected enum BonusMalus
		{
			Bonus = 1,
			Malus = -1
		}
		
		[SerializeField]
		protected int m_pointValue = 1;
		public XdeAsbStep EvaluatedStep { get; private set; }
		public Type MetricType => typeof(T);
		public int PointValue
		{
			get { return m_pointValue; }
			set { m_pointValue = value; }
		}

		public float BonusMalusPoints
		{
			get
			{
				if (BonusMalusLogic == (int)BonusMalus.Malus)
				{
					return (GetScore() - PointValue);
				}
				return GetScore();; 
				
			}
		}
		
		public abstract int BonusMalusLogic { get; }
		public abstract string GetLogs(bool p_displayPoints = false);
		public abstract float GetScore();
		
		#region MonoBehaviour
		protected virtual void OnEnable()
		{
			FillEvaluatedStep();
			
			EvaluatedStep.activationEvent.AddListener(OnActivate);
			EvaluatedStep.completedEvent.AddListener(OnComplete);
			EvaluatedStep.uncompletedEvent.AddListener(OnUncomplete);

			// If XdeAsbStep already fire its activeEvent => call OnActivate
			if (EvaluatedStep.IsActive)
			{
				OnActivate(EvaluatedStep);
			}
		}
		
		protected virtual void OnDisable()
		{
			EvaluatedStep.activationEvent.RemoveListener(OnActivate);
			EvaluatedStep.completedEvent.RemoveListener(OnComplete);
			EvaluatedStep.uncompletedEvent.RemoveListener(OnUncomplete);
		}
		#endregion

		public void FillEvaluatedStep()
		{
			if (this.transform.parent == null)
			{
				throw new Exception("No Parent for StepEvaluation");
			}
			
			EvaluatedStep = transform.GetComponentInParent<XdeAsbStep>() ?? throw new Exception("No XdeAsStep for StepEvaluation child of "+this.transform.parent.name);
			if (EvaluatedStep == null)
			{
				GameObject l_emptyGo = new GameObject();
				EvaluatedStep = l_emptyGo.gameObject.AddComponent<XdeAsbStep>();
				EvaluatedStep.name = "Empty_XdeAsbStep";
			}
		}
		
		protected virtual void OnActivate(XdeAsbStep p_step)
		{
		}
		protected virtual void OnComplete(XdeAsbStep p_step)
		{
		}
		
		protected virtual void OnUncomplete(XdeAsbStep p_step)
		{
		}

		protected string ToString(string p_category, bool p_displayPoints, string p_comments = null)
		{
			string l_result = string.Empty;
			
			// Get score
			string p_scoreStr = string.Empty;
			if (PointValue != 0)
			{
				float p_score = BonusMalusPoints;
				p_scoreStr += p_score.ToString();
			}
			else
			{
				p_comments = "[not evaluated]\n"+p_comments;
			}

			// Fill pattern
			if (p_displayPoints)
			{
				l_result += p_scoreStr + " ";
			}
			
			l_result += p_category;
			if (!string.IsNullOrEmpty(p_comments))
			{
				l_result += " : "+p_comments;
			}
			return l_result;
		}
	}
}

