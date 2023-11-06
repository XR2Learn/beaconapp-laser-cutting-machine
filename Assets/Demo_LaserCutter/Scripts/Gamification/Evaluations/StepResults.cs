//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using Gamification.Evaluations.Metrics;
using XdeEngine.Assembly;

namespace Gamification.Evaluations
{
	public class StepResults
	{
		private readonly XdeAsbStep m_step;
		/*Chronometer can be null according to designer choices*/
		private readonly Chronometer m_chronometer;
		private readonly IDictionary<Type,bool> m_metricsResult;

		public XdeAsbStep Step => m_step;
		public Chronometer Chronometer => m_chronometer;
		public IDictionary<Type,bool> MetricsResult => m_metricsResult;

		public StepResults(XdeAsbStep p_step, Chronometer p_chronometer, IDictionary<Type,bool> p_results)
		{
			m_step = p_step ?? throw new ArgumentNullException(nameof(p_step));
			m_metricsResult = p_results ?? throw new ArgumentNullException(nameof(p_results));
			m_chronometer = p_chronometer;
		}
	}
}

