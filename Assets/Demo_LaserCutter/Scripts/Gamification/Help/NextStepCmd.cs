//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Gamification.Extention;
using UnityEngine;
using XdeEngine.Assembly;
using Object = System.Object;

namespace Gamification.Help
{
	public class NextStepCmd : Help, IAsyncCmd
	{
		private XdeAsbScenario m_mainScenario;
		public NextStepCmd(GamificationController p_gamification, HelpController p_helpController) : base(p_gamification, p_helpController)
		{
			m_mainScenario = p_gamification.MainScenario ?? throw new ArgumentNullException(nameof(p_gamification.MainScenario));
		}
		
		public async Task Execute()
		{
			Debug.Log("[NextStepCmd] Execute ");

			if (!m_mainScenario.IsCompleted)
			{
				List<XdeAsbStep> l_passedSteps = m_mainScenario.GetActiveSteps();
				m_evaluation.AddAbandonedSteps(l_passedSteps);

				m_mainScenario.stopUpdate = true;
				await m_mainScenario.GoToNextStepAsync();
				m_mainScenario.stopUpdate = false;
			}
		}
	}
}

