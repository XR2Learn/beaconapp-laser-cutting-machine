//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using Gamification.Evaluations;
using XdeEngine.Assembly;

namespace Gamification.Help
{
	public abstract class Help
	{
		protected Evaluation m_evaluation;
		protected HelpScoringScale m_helpScoringScale;

		public Help(GamificationController p_gamification, HelpController p_helpController)
		{
			m_evaluation = p_gamification.Evaluation ?? throw new ArgumentNullException(nameof(p_gamification.Evaluation));
			m_helpScoringScale = p_helpController.HelpScoringScale ?? throw new ArgumentNullException(nameof(p_helpController.HelpScoringScale));
		}
	}
}
