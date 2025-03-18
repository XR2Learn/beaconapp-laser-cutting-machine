//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using Gamification.Evaluations.Metrics;
using UnityEngine;
using XdeEngine.Assembly;

namespace Gamification.Evaluations
{
	public class Chronometer : StepEvaluator<EfficiencyMetric>
	{
		[SerializeField]
		[Tooltip("Time value in seconds")]
		private float m_estimatedDuration = 30;
		private float m_startupTime = 0;
		private float m_timeElapsed = 0;
		private bool m_isRunning = false;
		public float TimeElapsed => m_timeElapsed;
	
		public float EstimatedDuration
		{
			get { return m_estimatedDuration; }
			set
			{
				if (value>0)
				{
					m_estimatedDuration = value;
				}
			}
		}
		private void Update()
		{
			if (this.m_isRunning)
				this.m_timeElapsed = Time.time - this.m_startupTime;
		}
		
		private void Run()
		{
			if (this.m_isRunning)
				return;
			
			this.m_startupTime = Time.time;
			this.m_isRunning = true;
		}
	
		private void Stop()
		{
			this.m_isRunning = false;
		}
	
		public override int BonusMalusLogic
		{
			get
			{
				return  (int)BonusMalus.Bonus;
			}
		}
	
		public override string GetLogs(bool p_displayPoints = false)
		{
			return ToString("Efficiency", p_displayPoints,
				"Last : "+m_timeElapsed.ToString("0.00")+" sec. (recommended : "+m_estimatedDuration+" sec)");
		}
	
		public override float GetScore()
		{
			if (EvaluatedStep.IsCompleted && this.m_timeElapsed <= this.m_estimatedDuration)
			{
				return m_pointValue;
			}
			
			return 0;
		}
		
		protected override void OnActivate(XdeAsbStep p_step)
		{
			base.OnActivate(p_step);
			Run();
		}
	
		protected override void OnComplete(XdeAsbStep p_step)
		{
			base.OnComplete(p_step);
			Stop();
		}
	}
}

