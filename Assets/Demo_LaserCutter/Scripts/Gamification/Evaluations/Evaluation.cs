//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gamification.Evaluations.Metrics;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.Evaluations
{
	public class Evaluation : MonoBehaviour
	{
		private List<StepResults> m_stepsResults;
		private Dictionary<Metric, double> m_metricsScore;
		private List<IStepEvaluator> m_stepEvaluators;
		private List<XdeAsbStep> m_abandonedSteps;
		private bool m_isEnded;

		public IEnumerable<Metric> Metrics { get; private set; }

		public string Logs { get; private set; }

		public double Score { get; private set; }

		public IReadOnlyList<StepResults> StepsResults => m_stepsResults;

		public IReadOnlyDictionary<Metric, double> MetricsScore => m_metricsScore;
		
		private void ComputeScore()
		{
			double l_scoreSum = 0;
			int l_coefficientSum = 0;
			
			this.m_metricsScore = new Dictionary<Metric, double>();

			foreach (Metric l_metric in this.Metrics)
			{
				// Get all evaluation steps evaluation except those for which the XdeAsbStep has been abandoned
				// if XdeAsbStep has been abandoned its score equals 0 => so it doesn't take part in the sum
				IEnumerable<IStepEvaluator> l_metricSteps = this.m_stepEvaluators.Where(x => x.MetricType == l_metric.GetType());
				IEnumerable<IStepEvaluator> l_notAbandonedMetricSteps = l_metricSteps.Where(x => !m_abandonedSteps.Contains(x.EvaluatedStep));
				
				int l_maximalPoints = l_metricSteps.Sum(x => x.PointValue);
				l_maximalPoints = l_maximalPoints==0? 1:l_maximalPoints;
				
				float l_traineePoints = l_notAbandonedMetricSteps.Sum(x => x.GetScore());
				double l_metricScrore = 100 * ((double) l_traineePoints / l_maximalPoints);
				m_metricsScore.Add(l_metric, l_metricScrore);
				
				l_scoreSum += l_metric.Coefficient * l_metricScrore;
				l_coefficientSum += l_metric.Coefficient;
			}
			l_coefficientSum = l_coefficientSum==0? 1:l_coefficientSum;
			this.Score = (double)l_scoreSum / l_coefficientSum;
		}
		
		private void ComputeLogs()
		{
			var l_logBuilder = new StringBuilder();
			
			l_logBuilder.Append("[Evaluation] ComputeLogs - ** LOGS ** Steps comments : \n\n");
			var l_groupSteps = m_stepEvaluators.GroupBy(x => x.EvaluatedStep);
			foreach (var l_stepEvaluations in l_groupSteps)
			{
				l_logBuilder.Append(l_stepEvaluations.Key.name);
				l_logBuilder.AppendLine(":");
				
				if (m_abandonedSteps.Contains(l_stepEvaluations.Key))
				{
					l_logBuilder.Append("+0 Step abandoned !");
				}
				else
				{
					foreach (IStepEvaluator l_evaluation in l_stepEvaluations)
					{
						l_logBuilder.Append(l_evaluation.GetLogs(true));
						l_logBuilder.AppendLine(":");
					}
				}
				l_logBuilder.Append(" \n\n");
			}
			
			this.Logs = l_logBuilder.ToString();
		}
		
		private void ComputeResults()
		{
			this.m_stepsResults = new List<StepResults>();

			var l_groups = m_stepEvaluators.GroupBy(x => x.EvaluatedStep);
			foreach (var l_group in l_groups)
			{
				Chronometer l_chronometer = l_group.ToList().OfType<Chronometer>().FirstOrDefault();
				IDictionary<Type, bool> l_metricsResults = GetStepResults( l_group.Key);
				StepResults l_stepResults = new StepResults(l_group.Key,l_chronometer,l_metricsResults);
				
				this.m_stepsResults.Add(l_stepResults);
			}
		}

		public void Init(IList<XdeAsbScenario> p_scenarios)
		{
			if (p_scenarios == null)
			{
				throw new ArgumentNullException(nameof(p_scenarios));
			}
			
			m_abandonedSteps = new List<XdeAsbStep>();
			Metrics = GetComponentsInChildren<Metric>().ToList();
			m_stepEvaluators = GetAllStepsEvaluators(p_scenarios);

			m_isEnded = false;
		}

		public void EndEvaluation()
		{
			if (!m_isEnded)
			{
				this.ComputeScore();
				this.ComputeLogs();
				this.ComputeResults();

				Debug.Log(this.Logs);
				
				this.m_isEnded = true;
			}
			
		}

		public bool HasBeenAbandoned(XdeAsbStep p_step)
		{
			return m_abandonedSteps.Contains(p_step);
		}
		
		public List<IStepEvaluator> GetAllStepsEvaluators(IList<XdeAsbScenario> p_parents)
		{
			if (p_parents == null)
			{
				throw new ArgumentNullException(nameof(p_parents));
			}
			List<IStepEvaluator> l_evaluationSteps = new List<IStepEvaluator>();
			foreach (XdeAsbScenario l_parent in p_parents)
			{
				l_evaluationSteps.AddRange(l_parent.GetComponentsInChildren<IStepEvaluator>(true));
			}
			
			return l_evaluationSteps;
		}

		public IList<I> GetStepsEvaluators<I>(IList<XdeAsbStep> p_activeSteps = null) where I:IStepEvaluator
		{
			if (p_activeSteps == null)
			{
				return m_stepEvaluators.OfType<I>().ToList();
			}
			else
			{
				return m_stepEvaluators.OfType<I>().Where(x=> p_activeSteps.Contains(x.EvaluatedStep)).Select(x=> x).ToList();
			}
		}
		
		// Can return a null value
		public I GetStepEvaluator<I>(XdeAsbStep p_step) where I:IStepEvaluator
		{
			return m_stepEvaluators.OfType<I>().FirstOrDefault(x=>x.EvaluatedStep == p_step);
		}

		public IDictionary<Type, bool> GetStepResults(XdeAsbStep p_step)
		{
			bool l_metricResult;
			Dictionary<Type, bool> l_metricsResults = new Dictionary<Type, bool>();
			
			List<IStepEvaluator> l_stepEvaluators = m_stepEvaluators.FindAll(x=>x.EvaluatedStep==p_step);
			foreach (IStepEvaluator l_evaluation in l_stepEvaluators)
			{
				if (l_evaluation.PointValue != 0)
				{
					l_metricResult = l_evaluation.BonusMalusLogic==1 ? (l_evaluation.GetScore() > 0) : (l_evaluation.GetScore() == l_evaluation.PointValue);
					l_metricResult = m_abandonedSteps.Contains(p_step) ? false : l_metricResult;
					
					l_metricsResults.Add(l_evaluation.MetricType,l_metricResult);
					//Debug.Log("-------------- Step:"+p_step.name+" Metric: " + l_evaluation.MetricType+"  / gain? "+l_metricResult+" /l_evaluation.BonusMalusLogic "+l_evaluation.BonusMalusLogic);
				}
			}
			return l_metricsResults;
		}

		public void AddAbandonedSteps(IEnumerable<XdeAsbStep> steps)
		{
			this.m_abandonedSteps.AddRange(steps);
		}
	}
}

