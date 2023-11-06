//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using Gamification.Evaluations.Metrics;
using Gamification.Help;

namespace Gamification.Evaluations
{
	public class HelpConsumer : StepEvaluator<AutonomyMetric>
	{
		private float m_score;
		private string m_logs = string.Empty;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_score = PointValue;
		}

		public override float GetScore()
		{
			return m_score;
		}

		public override int BonusMalusLogic
		{
			get
			{
				return (int)BonusMalus.Malus;
			}
		}

		public override string GetLogs(bool p_displayPoints = false)
		{
			if (string.IsNullOrEmpty(m_logs))
			{
				m_logs += "No help requested";
			}
			return ToString("Autonomy",p_displayPoints,m_logs);
		}

		public void ConsumeHelp(VisualGuidesCmd p_help, HelpScoringScale p_helpScoringScale)
		{
			m_logs += "consume VisualGuide help/";
			m_score -= (float)PointValue * p_helpScoringScale.VisualGuidesCoef / p_helpScoringScale.CoefSum;
		}
		
		public void ConsumeHelp(TodoListDisplayCmd p_help, HelpScoringScale p_helpScoringScale)
		{
			m_logs += "consume TodoListDisplay help/";
			m_score -= (float)PointValue * p_helpScoringScale.TodoListCoef / p_helpScoringScale.CoefSum;
		}
	}
}